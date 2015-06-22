using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.PhotoGallery;
using Model;
using System.Web;
using System.IO;
using ADOPets.Web.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.Security;
using System.Linq.Expressions;
using System.Collections.Generic;
using ADOPets.Web.ViewModels.GalleryComment;
namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class PhotoGalleryController : BaseController
    {
        public ActionResult NewsFeedNotification()
        {
            return PartialView("_NewsFeedNotification");
        }

        [HttpGet]
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            var galleryList = UnitOfWork.GalleryRepository.GetAll(g => g.PetId == petId && !g.IsDeleted && g.IsGalleryPhoto, navigationProperties: c => c.Pet).Select(g => new IndexViewModel(g, ImageToByteArray(g.ImageURL)));
            Session["count"] = 0;

            return PartialView("_Index", galleryList);
        }

        [HttpGet]
        public ActionResult Add(int petId)
        {
            return PartialView("_Add", new AddViewModel(petId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add( AddViewModel model,FormCollection fc)
        {
            var userId = HttpContext.User.GetUserId();
          //  AddViewModel model = new AddViewModel();
            model.Title = fc["Title"];
            model.PetId = Convert.ToInt32(fc["PetId"]);
            string ownerId = userId.ToString();
            string ownerInfoPath = UnitOfWork.UserRepository.GetSingle(x => x.Id == userId).InfoPath;
            if (ModelState.IsValid)
            {

                var directoryPath = "";
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    try
                    {
                        HttpPostedFileBase imgFile = Request.Files[0];
                        if (imgFile != null && imgFile.ContentLength > 0)
                        {
                            string imgName = DateTime.Now.ToString("yyyymmddhhmmss") + "_" + Path.GetFileName(imgFile.FileName);
                            directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.GalleryPhotoFolderName);
                            model.ImagePath = imgName;
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            var path = Path.Combine(directoryPath, imgName);
                            GenerateThumbnails(0.5, imgFile.InputStream, path);

                            Image thumb = Image.FromStream(imgFile.InputStream, true, true);
                            Image thumbGalleryPhoto = thumb.GetThumbnailImage(300, 225, null, IntPtr.Zero);
                            var thumbdirectoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.ThumbnailGalleryPhotoFolderName);
                            if (!Directory.Exists(thumbdirectoryPath))
                            {
                                Directory.CreateDirectory(thumbdirectoryPath);
                            }
                            string thumbImgName = "Thumb_" + imgName;
                            path = Path.Combine(thumbdirectoryPath, thumbImgName);
                            thumbGalleryPhoto.Save(path);
                            thumbGalleryPhoto.Dispose();
                        }
                    }
                    catch { }
                }
                if (!string.IsNullOrEmpty(model.ImagePath))
                {
                    UnitOfWork.GalleryRepository.Insert(model.Map());
                    UnitOfWork.Save();
                    Success(Resources.Wording.Gallery_Add_AddSuccessMessage);
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Edit(int ImageId)
        {
            var gallery = UnitOfWork.GalleryRepository.GetSingle(g => g.Id == ImageId);
            return PartialView("_Edit", new EditViewModel(gallery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel mod, FormCollection fc)
        {
            EditViewModel model = new EditViewModel();
            model.Title = fc["Title"];
            model.PetId = Convert.ToInt32(fc["PetId"]);
            model.Id = Convert.ToInt32(fc["Id"]);
            if (ModelState.IsValid)
            {
                var gallery = UnitOfWork.GalleryRepository.GetSingleTracking(g => g.Id == model.Id);
                model.Map(gallery);

                UnitOfWork.GalleryRepository.Update(gallery);
                UnitOfWork.Save();
                Success(Resources.Wording.Gallery_Edit_UpdateSuccessMessage);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Delete(int ImageId)
        {
            var gallery = UnitOfWork.GalleryRepository.GetSingle(g => g.Id == ImageId);
            return PartialView("_Delete", new DeleteViewModel(gallery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var albumPhoto = UnitOfWork.AlbumGalleryRepository.GetSingle(g => g.PetId == petId && g.AlbumGallery_Photo.Any(gp => gp.GalleryId == id), navigationProperties: gp => gp.AlbumGallery_Photo);
            if (albumPhoto != null)
            {
                var gallery = UnitOfWork.GalleryRepository.GetSingle(c => c.Id == id);
                gallery.IsDeleted = true;
                UnitOfWork.GalleryRepository.Update(gallery);
                UnitOfWork.Save();
            }
            else
            {
                var gallery = UnitOfWork.GalleryRepository.GetSingle(c => c.Id == id);
                UnitOfWork.GalleryRepository.Delete(gallery);
                UnitOfWork.Save();
            }
            Success(Resources.Wording.Gallery_Delete_DeleteSuccessMessage);
            return RedirectToAction("Index", new { petId = petId });
        }

        [HttpGet]
        public ActionResult GetImage(string fileName, string imgType, int petId)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string ownerId = "";
                    string ownerInfoPath = "";
                    var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == petId, p => p.Insurances, p => p.Farmer, p => p.PetContacts, p => p.Veterinarians, p => p.Users, p => p.SharePetInformations);
                    if (pet == null)
                    {
                        return RedirectToAction("Index", "Pet");
                    }
                    else
                    {
                        if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                        {
                            var petOwner = pet.Users.FirstOrDefault();
                            if (petOwner != null)
                            {
                                ownerId = Convert.ToString(petOwner.Id);
                                ownerInfoPath = petOwner.InfoPath;
                            }
                        }
                    }

                    string fileNameNew = "";
                    var directoryPath = "";
                    if (!string.IsNullOrEmpty(imgType))
                    {
                        directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.ThumbnailGalleryPhotoFolderName);
                        fileNameNew = "Thumb_" + fileName;
                    }
                    else
                    {
                        directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.GalleryPhotoFolderName);
                        var splitExt = fileName.Split('.');
                        var ext = splitExt[1].ToUpper();
                        fileNameNew = splitExt[0] + "." + ext;
                    }
                    var path = Path.Combine(directoryPath, fileNameNew);
                    var exist = System.IO.File.Exists(path);
                    if (exist)
                    {
                        return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), "image/*");
                    }
                    else
                    {
                        String RelativePath = Server.MapPath("~/Content/images/animal-dimg.jpg");// : Server.MapPath("~/Content/images/animal-dimg.jpg");
                        var fs = new FileStream(RelativePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        return new FileStreamResult(fs, "image/*");
                    }
                }
                else
                {
                    String RelativePath = Server.MapPath("~/Content/images/animal-dimg.jpg");// : Server.MapPath("~/Content/images/animal-dimg.jpg");
                    var fs = new FileStream(RelativePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    return new FileStreamResult(fs, "image/*");
                }
            }
            catch { return null; }
        }

        public byte[] ImageToByteArray(string imagefilePath)
        {
            var userId = HttpContext.User.GetUserId();
            string ownerId = userId.ToString();
            string ownerInfoPath = UnitOfWork.UserRepository.GetSingle(x => x.Id == userId).InfoPath;

            if (!string.IsNullOrEmpty(imagefilePath))
            {
                var directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.GalleryPhotoFolderName);
                var path = Path.Combine(directoryPath, imagefilePath);
                try
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                    byte[] imageByte = ImageToByteArraybyImageConverter(image);
                    return imageByte;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        private byte[] ImageToByteArraybyImageConverter(System.Drawing.Image image)
        {
            ImageConverter imageConverter = new ImageConverter();
            byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            return imageByte;
        }

        public void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }

        [HttpGet]
        public ActionResult PhotoDetails(int imageId, int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;

            ViewBag.Likes = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == imageId && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
            ViewBag.Comments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == imageId && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();

            var glist = UnitOfWork.GalleryRepository.GetAll(x => x.PetId == petId && x.Id < imageId && !x.IsDeleted && x.IsGalleryPhoto).ToList();
            ViewBag.Last = glist.LastOrDefault() == null ? false : glist.LastOrDefault().Id == imageId ? true : false;
            ViewBag.First = glist.FirstOrDefault() == null ? false : glist.FirstOrDefault().Id == imageId ? true : false;

            //Uri uri = System.Web.HttpContext.Current.Request.Url;
           // string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetImage?galleryId=";
            string qapathSys = System.Configuration.ConfigurationManager.AppSettings["WebsitePath"];
            string host = qapathSys + "/Account/GetImage?galleryId=";

            var galleryList = UnitOfWork.GalleryRepository.GetAll(g => g.Id == imageId && g.PetId == petId && !g.IsDeleted && g.IsGalleryPhoto, navigationProperties: c => c.Pet).Select(g => new IndexViewModel(g, host)).FirstOrDefault();
            if (galleryList == null)
            {
                return RedirectToAction("Index", "Pet");
            }
            return View("PhotoDetails", galleryList);
        }

        [HttpGet]
        public ActionResult PrevNextImage(int imageId, int petId, string type)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            int? Imgid = 0;
            bool flag = false;
            var glist = UnitOfWork.GalleryRepository.GetAll(x => x.PetId == petId && !x.IsDeleted && x.IsGalleryPhoto).ToList();
            foreach (var gallery in glist)
            {
                if (flag)
                {
                    Imgid = gallery.Id;
                    break;
                }
                if (gallery.Id == imageId && type == "next")
                {
                    flag = true;
                }
                if (gallery.Id == imageId && type == "prev")
                {
                    break;
                }
                Imgid = gallery.Id;
            }
            ViewBag.Likes = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == Imgid && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
            ViewBag.Comments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == Imgid && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();

            ViewBag.Last = glist.LastOrDefault() == null ? false : glist.LastOrDefault().Id == Imgid ? true : false;
            ViewBag.First = glist.FirstOrDefault() == null ? false : glist.FirstOrDefault().Id == Imgid ? true : false;
            Uri uri = System.Web.HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetImage?galleryId=";
            var galleryList = UnitOfWork.GalleryRepository.GetAll(g => g.Id == Imgid && g.PetId == petId && !g.IsDeleted && g.IsGalleryPhoto, navigationProperties: c => c.Pet).Select(g => new IndexViewModel(g, host)).FirstOrDefault();
            if (galleryList == null)
            {
                return RedirectToAction("Index", "Pet");
            }
            return PartialView("_Details", galleryList);
        }
    }
}
