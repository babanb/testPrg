using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.AlbumGallery;
using Model;
using System.Web;
using System.IO;
using ADOPets.Web.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using Repository.Implementations;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq.Expressions;
using ADOPets.Web.ViewModels.GalleryComment;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class AlbumGalleryController : BaseController
    {
        public ActionResult getUploadedFile(string filename)
        {
            var userId = HttpContext.User.GetUserId();
            string ownerId = userId.ToString();
            string ownerInfoPath = UnitOfWork.UserRepository.GetSingle(x => x.Id == userId).InfoPath;
            if (!string.IsNullOrEmpty(filename))
            {
                var dirPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.GalleryPhotoFolderName);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                string path = Path.Combine(dirPath, Path.GetFileName(filename));

                var exist = System.IO.File.Exists(path);

                if (exist)
                {
                    return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), "image/*");
                }
            }
            return null;
        }

        [HttpGet]
        public ActionResult Index(int petId)
        {
            var galleryList = new List<IndexViewModel>();
            var dbAlbum = UnitOfWork.AlbumGalleryRepository.GetAll(p => p.PetId == petId, navigationProperties: p => p.AlbumGallery_Photo);
            foreach (var album in dbAlbum)
            {
                List<Gallery> photo = new List<Gallery>();
                Gallery defaultPhoto = null;
                List<AlbumGallery_Photo> lstGallery = album.AlbumGallery_Photo.ToList();
                if (lstGallery != null && lstGallery.Count > 0)
                {
                    foreach (AlbumGallery_Photo dbGallery in lstGallery)
                    {
                        Gallery gal = UnitOfWork.GalleryRepository.GetSingle(g => g.Id == dbGallery.GalleryId);
                        if (Convert.ToBoolean(dbGallery.IsDefaultAlbumCover))
                        {
                            defaultPhoto = gal;
                        }
                        photo.Add(gal);
                    }

                    if (defaultPhoto == null)
                    {
                        if (photo != null && photo.Count > 0)
                        {
                            defaultPhoto = photo[0];
                        }
                    }
                    galleryList.Add(new IndexViewModel(album, defaultPhoto.ImageURL, photo.Count));
                }
            }
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;

            Session["count"] = 0;

            return PartialView("_Index", galleryList);
        }

        [HttpGet]
        public ActionResult Add(int petId)
        {
            return PartialView("_Add", new AddViewModel(petId));
        }

        public ActionResult ListPhotoGallery(int petId, int albumId = 0)
        {
            List<PhotoListViewModel> lstPhotoList = null;
            if (albumId == 0)
            {
                lstPhotoList = UnitOfWork.GalleryRepository.GetAll(g => g.PetId == petId && !g.IsDeleted && g.IsGalleryPhoto)
                    .Select(g => new PhotoListViewModel(g, false)).ToList();
            }
            else
            {
                lstPhotoList = UnitOfWork.GalleryRepository.GetAll(g => g.PetId == petId && !g.IsDeleted && g.IsGalleryPhoto)
                .Select(g => new PhotoListViewModel(g, IsInAlbumGallery(g.Id, albumId))).ToList();
            }
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            Session["count"] = 0;

            return PartialView("_ListPhotoGallery", lstPhotoList);
        }

        [HttpGet]
        public ActionResult DeletePhoto(int ImageId, string currentAlbumId)
        {
            ViewBag.AlbumId = currentAlbumId;
            TempData["AlbumIdDelete"] = currentAlbumId;
            var gallery = UnitOfWork.GalleryRepository.GetSingle(g => g.Id == ImageId);
            return PartialView("_DeletePhoto", new ViewModels.PhotoGallery.DeleteViewModel(gallery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(int id, int petId)
        {
            int albumId = Convert.ToInt32(TempData["AlbumIdDelete"]);
            var album = UnitOfWork.AlbumGalleryRepository.GetAll(g => g.AlbumGallery_Photo.Any(a => a.GalleryId == id) && g.PetId == petId && g.Id == albumId, navigationProperties: g => g.AlbumGallery_Photo);
            if (album != null && album.Count() > 0)
            {
                AlbumGallery albumGallery = album.FirstOrDefault();

                //if (albumGallery.AlbumGallery_Photo.First().IsDefaultAlbumCover) {

                //}

                UnitOfWork.AlbumGalleryRepository.ExecuteSqlCommand("Delete From AlbumGallery_Photo where AlbumId={0} and GalleryId={1}", albumGallery.Id, id);
                UnitOfWork.Save();
                try
                {
                    var gallery = UnitOfWork.GalleryRepository.GetSingle(c => c.Id == id && !c.IsGalleryPhoto);
                    if (gallery != null)
                    {
                        UnitOfWork.GalleryRepository.Delete(gallery);
                        UnitOfWork.Save();
                    }
                }
                catch { }
            }

            Success(Resources.Wording.Gallery_Delete_DeleteSuccessMessage);
            return RedirectToAction("Index", new { petId = petId });
        }

        private bool IsInAlbumGallery(int galleryPhotoId, int albumId)
        {
            var exists = UnitOfWork.AlbumGalleryRepository.GetAll(ag => ag.AlbumGallery_Photo.Any(a => a.GalleryId == galleryPhotoId) && ag.Id == albumId);
            if (exists != null && exists.Count() > 0) { return true; }
            return false;
        }

        [HttpPost]
        public JsonResult CreateAlbumGallery(AddViewModel addViewModel, string[] deletedPhoto)
        {
            var userId = HttpContext.User.GetUserId();
            string ownerId = userId.ToString();
            string ownerInfoPath = UnitOfWork.UserRepository.GetSingle(x => x.Id == userId).InfoPath;
            List<AlbumGallery_Photo> lstAlbumPhoto = new List<AlbumGallery_Photo>();
            List<Gallery> lstNewGallery = new List<Gallery>();

            foreach (Gallery gallery in addViewModel.lstGallery)
            {
                AlbumGallery_Photo albumGalleryPhoto = new AlbumGallery_Photo();

                albumGalleryPhoto.IsDefaultAlbumCover = (gallery.ImageURL.Contains(addViewModel.IsDefault.ToString())) ? true : false;
                if (gallery.Id == 0)
                {
                    gallery.CreatedDate = DateTime.Now;
                    UnitOfWork.GalleryRepository.Insert(gallery);
                    UnitOfWork.Save();
                }
                albumGalleryPhoto.GalleryId = gallery.Id;
                lstAlbumPhoto.Add(albumGalleryPhoto);
            }

            AlbumGallery albumGalleryModel = new AlbumGallery();
            albumGalleryModel.CreatedDate = DateTime.Now;
            albumGalleryModel.Title = addViewModel.Title;
            albumGalleryModel.PetId = addViewModel.PetId;
            albumGalleryModel.AlbumGallery_Photo = lstAlbumPhoto;
            UnitOfWork.AlbumGalleryRepository.Insert(albumGalleryModel);
            UnitOfWork.Save();
            Success(Resources.Wording.AlbumGallery_Add_AddSuccessMessage);
            if (deletedPhoto != null && deletedPhoto.Count() > 0)
            {
                foreach (string newfileName in deletedPhoto)
                {
                    var dirPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.GalleryPhotoFolderName);
                    var path = Path.Combine(dirPath, newfileName);
                    var exist = System.IO.File.Exists(path);

                    if (exist)
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }

            return Json(new { success = "_Index" });
        }

        [HttpGet]
        public ActionResult Delete(int albumId)
        {
            var gallery = UnitOfWork.AlbumGalleryRepository.GetSingle(g => g.Id == albumId);
            return PartialView("_Delete", new DeleteViewModel(gallery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            List<int> lstGalleryId = new List<int>();
            var albumPhoto = UnitOfWork.AlbumGalleryRepository.GetSingle(g => g.PetId == petId && g.Id == id, navigationProperties: gp => gp.AlbumGallery_Photo);
            if (albumPhoto != null)
            {
                foreach (AlbumGallery_Photo agp in albumPhoto.AlbumGallery_Photo)
                {
                    int galleryId = Convert.ToInt32(agp.GalleryId);
                    if (galleryId > 0) { lstGalleryId.Add(galleryId); }
                }

                UnitOfWork.AlbumGalleryRepository.Delete(albumPhoto);
                UnitOfWork.Save();
                foreach (int galId in lstGalleryId)
                {
                    Gallery gal = UnitOfWork.GalleryRepository.GetSingle(g => g.Id == galId && !g.IsGalleryPhoto);
                    if (gal != null)
                    {
                        try
                        {
                            UnitOfWork.GalleryRepository.Delete(gal); 
                            UnitOfWork.Save();
                        }
                        catch 
                        {
                            
                        }
                    }
                }
            }
            Success(Resources.Wording.AlbumGallery_Delete_DeleteSuccessMessage);
            return RedirectToAction("Index", new { petId = petId });
        }

        [HttpGet]
        public ActionResult Edit(int albumId)
        {
            var albumPhoto = UnitOfWork.AlbumGalleryRepository.GetSingle(g => g.Id == albumId, navigationProperties: gp => gp.AlbumGallery_Photo);
            string isDefault = "";
            List<Gallery> lstGallery = new List<Gallery>();

            foreach (AlbumGallery_Photo agp in albumPhoto.AlbumGallery_Photo)
            {
                Gallery gal = UnitOfWork.GalleryRepository.GetSingle(g => g.Id == agp.GalleryId);
                if (agp.IsDefaultAlbumCover)
                {
                    isDefault = gal.ImageURL;
                }

                if (albumPhoto.AlbumGallery_Photo.LastOrDefault().Id == agp.Id)
                {
                    if (string.IsNullOrEmpty(isDefault))
                    {
                        isDefault = gal.ImageURL;
                    }
                }
                lstGallery.Add(gal);
            }
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = albumPhoto.PetId;
            return PartialView("_Edit", new EditViewModel(albumPhoto, lstGallery, isDefault));
        }

        [HttpPost]
        public JsonResult EditAlbumGallery(EditViewModel editViewModel, string[] deletedPhoto)
        {
            var userId = HttpContext.User.GetUserId();
            string ownerId = userId.ToString();
            string ownerInfoPath = UnitOfWork.UserRepository.GetSingle(x => x.Id == userId).InfoPath;
            List<AlbumGallery_Photo> lstAlbumPhoto = new List<AlbumGallery_Photo>();
            List<Gallery> lstNewGallery = new List<Gallery>();

            var gallery = UnitOfWork.AlbumGalleryRepository.GetSingleTracking(g => g.Id == editViewModel.Id, navigationProperties: ag => ag.AlbumGallery_Photo);
            var lstOLDDBAlbumPhoto = gallery.AlbumGallery_Photo;

            editViewModel.Map(gallery);

            foreach (Gallery gal in editViewModel.lstGallery)
            {
                AlbumGallery_Photo albumGalleryPhoto = new AlbumGallery_Photo();

                albumGalleryPhoto.IsDefaultAlbumCover = (gal.ImageURL.Contains(editViewModel.IsDefaultCover.ToString())) ? true : false;
                if (gal.Id == 0)
                {
                    gal.CreatedDate = DateTime.Now;
                    UnitOfWork.GalleryRepository.Insert(gal);
                    UnitOfWork.Save();
                }
                else
                {
                    gal.CreatedDate = DateTime.Now;
                    UnitOfWork.GalleryRepository.Update(gal);
                    UnitOfWork.Save();
                }
                albumGalleryPhoto.GalleryId = gal.Id;
                albumGalleryPhoto.AlbumId = editViewModel.Id;
                lstAlbumPhoto.Add(albumGalleryPhoto);
            }

            gallery.AlbumGallery_Photo = lstAlbumPhoto;

            // get id to delete from albumphoto gallery table
            var photoToBeDeleted = lstOLDDBAlbumPhoto.Where(p => !gallery.AlbumGallery_Photo.Any(p2 => p2.GalleryId == p.GalleryId));

            UnitOfWork.AlbumGalleryRepository.Update(gallery);
            UnitOfWork.Save();

            if (photoToBeDeleted != null && photoToBeDeleted.Count() > 0)
            {
                foreach (var p in photoToBeDeleted)
                {
                    UnitOfWork.AlbumGalleryRepository.ExecuteSqlCommand("Delete From AlbumGallery_Photo where AlbumId={0} and GalleryId={1}", p.AlbumId, p.GalleryId);
                    UnitOfWork.Save();
                }
            }

            Success(Resources.Wording.AlbumGallery_Edit_UpdateSuccessMessage);
            if (deletedPhoto != null && deletedPhoto.Count() > 0)
            {
                foreach (string newfileName in deletedPhoto)
                {
                    if (!string.IsNullOrEmpty(newfileName))
                    {
                        var dirPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(ownerInfoPath, ownerId), Constants.GalleryPhotoFolderName);
                        var path = Path.Combine(dirPath, newfileName);
                        var exist = System.IO.File.Exists(path);

                        if (exist)
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                }
            }
            return Json(new { success = "_Index" });
        }

        [HttpGet]
        public ActionResult ListAlbumPhoto(int albumId)
        {
            List<Gallery> lstPhotoList = new List<Gallery>();
            ViewBag.AlbumId = albumId.ToString();
            var album = UnitOfWork.AlbumGalleryRepository.GetSingle(ag => ag.Id == albumId, navigationProperties: AppDomain => AppDomain.AlbumGallery_Photo);

            foreach (var albumphoto in album.AlbumGallery_Photo)
            {
                Gallery gal = UnitOfWork.GalleryRepository.GetSingle(g => g.Id == albumphoto.GalleryId, navigationProperties: c => c.Pet);
                if (gal != null)
                {
                    lstPhotoList.Add(gal);
                }
            }
            //Uri uri = System.Web.HttpContext.Current.Request.Url;
            //string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetImage?galleryId=";
            string qapathSys = System.Configuration.ConfigurationManager.AppSettings["WebsitePath"];
            string host = qapathSys + "/Account/GetImage?galleryId=";
            var listPhoto = lstPhotoList.Select(a => new ViewModels.PhotoGallery.IndexViewModel(a, host));
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = album.PetId; 
            Session["count"] = 0;

            return PartialView("_ListAlbumPhoto", listPhoto);
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
                        var fs = (string.IsNullOrEmpty(imgType)) ? new FileStream(RelativePath, FileMode.Open) : Stream.Null;
                        return new FileStreamResult(fs, "image/*");
                    }
                }
                else
                {
                    String RelativePath = Server.MapPath("~/Content/images/animal-dimg.jpg");// : Server.MapPath("~/Content/images/animal-dimg.jpg");
                    var fs = (string.IsNullOrEmpty(imgType)) ? new FileStream(RelativePath, FileMode.Open) : Stream.Null;
                    return new FileStreamResult(fs, "image/*");
                }
            }
            catch { return null; }
        }

        [HttpGet]
        public ActionResult AlbumDetails(int imageId, int petId, int albumId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            ViewBag.AlbumId = albumId;
            ADOPets.Web.ViewModels.PhotoGallery.IndexViewModel galleryList = new ADOPets.Web.ViewModels.PhotoGallery.IndexViewModel();
            var album = UnitOfWork.AlbumGalleryRepository.GetSingle(ag => ag.Id == albumId, navigationProperties: AppDomain => AppDomain.AlbumGallery_Photo);
           // Uri uri = System.Web.HttpContext.Current.Request.Url;
           // string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetImage?galleryId=";
            string qapathSys = System.Configuration.ConfigurationManager.AppSettings["WebsitePath"];
            string host = qapathSys + "/Account/GetImage?galleryId=";
            if (album != null)
            {
                foreach (var albumphoto in album.AlbumGallery_Photo)
                {
                    if (albumphoto.GalleryId == imageId)
                    {
                        galleryList = UnitOfWork.GalleryRepository.GetAll(g => g.Id == imageId && g.PetId == petId && !g.IsDeleted, navigationProperties: c => c.Pet).Select(g => new ViewModels.PhotoGallery.IndexViewModel(g, host,albumId)).FirstOrDefault();
                    }
                }

            }
            if (album != null && album.AlbumGallery_Photo != null)
            {
                ViewBag.Last = album.AlbumGallery_Photo.LastOrDefault() == null ? false : album.AlbumGallery_Photo.LastOrDefault().GalleryId == imageId ? true : false;
                ViewBag.First = album.AlbumGallery_Photo.FirstOrDefault() == null ? false : album.AlbumGallery_Photo.FirstOrDefault().GalleryId == imageId ? true : false;
                ViewBag.Likes = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == imageId && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
                ViewBag.Comments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == imageId && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
            }
            else
            {
                ViewBag.Last = false;
                ViewBag.First = false;
                ViewBag.Likes = "0";
                ViewBag.Comments = "0";
            }
            if (galleryList == null)
            {
                return RedirectToAction("Index", "Pet");
            }
            return View("AlbumDetails", galleryList);
        }

        [HttpGet]
        public ActionResult PrevNextImage(int imageId, int petId, string type, int albumId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            ViewBag.AlbumId = albumId;

            ADOPets.Web.ViewModels.PhotoGallery.IndexViewModel galleryList = new ADOPets.Web.ViewModels.PhotoGallery.IndexViewModel();
            var album = UnitOfWork.AlbumGalleryRepository.GetSingle(ag => ag.Id == albumId, navigationProperties: AppDomain => AppDomain.AlbumGallery_Photo);
            int? Imgid = 0;
            bool flag = false;
            //Uri uri = System.Web.HttpContext.Current.Request.Url;
            //string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetImage?galleryId=";
            string qapathSys = System.Configuration.ConfigurationManager.AppSettings["WebsitePath"];
            string host = qapathSys + "/Account/GetImage?galleryId=";
            if (album != null && album.AlbumGallery_Photo != null)
            {
                foreach (var albumphoto in album.AlbumGallery_Photo)
                {
                    if (flag)
                    {
                        Imgid = albumphoto.GalleryId;
                        break;
                    }
                    if (albumphoto.GalleryId == imageId && type == "next")
                    {
                        flag = true;
                    }
                    if (albumphoto.GalleryId == imageId && type == "prev")
                    {
                        break;
                    }
                    Imgid = albumphoto.GalleryId;
                }
               

                ViewBag.Last = album.AlbumGallery_Photo.LastOrDefault() == null ? false : album.AlbumGallery_Photo.LastOrDefault().GalleryId == Imgid ? true : false;
                ViewBag.First = album.AlbumGallery_Photo.FirstOrDefault() == null ? false : album.AlbumGallery_Photo.FirstOrDefault().GalleryId == Imgid ? true : false;
                ViewBag.Likes = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == Imgid && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
                ViewBag.Comments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == Imgid && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
            }
            else
            {
                ViewBag.Last = false;
                ViewBag.First = false;
                ViewBag.Likes = "0";
                ViewBag.Comments = "0";
            }

            galleryList = UnitOfWork.GalleryRepository.GetAll(g => g.Id == Imgid && g.PetId == petId && !g.IsDeleted, navigationProperties: c => c.Pet).Select(g => new ViewModels.PhotoGallery.IndexViewModel(g, host, albumId)).FirstOrDefault();
            if (galleryList == null)
            {
                return RedirectToAction("Index", "Pet");
            }
            return PartialView("_Details", galleryList);
        }
    }
}
