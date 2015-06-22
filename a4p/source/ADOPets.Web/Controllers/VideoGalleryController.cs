﻿using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.ViewModels.VideoGallery;
using System.Data.Entity.Validation;
using System.Diagnostics;
using ADOPets.Web.Common.Authentication;
using Model;
using System.Collections.Generic;
using System.Linq.Expressions;
using ADOPets.Web.ViewModels.GalleryComment;
using System.IO;
using System.Web;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Configuration;
using ADOPets.Web.Common;
using Repository.Implementations;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class VideoGalleryController : BaseController
    {
        HttpPostedFileBase imgFile;
        private string inputVideo, outputVideoName;
        private string argsHigh, argsScreen;
        string fileName, testName, path, demopath, mypath, pathhigh, virtaulName;
        private bool highDone = false, screenDone = false;
        string imgOutputpath, Outputpath;
        Process ffmpeg, ffmpegScreen, exiftool;

        [HttpGet]
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            var myCountRecord = UnitOfWork.VideoGalleryRepository.GetAll(m => m.IsEncoded == false).Count();
            Session["Count"] = myCountRecord;
           // Uri uri = System.Web.HttpContext.Current.Request.Url;
            //string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetVideo?galleryId=";
            string qapathSys = System.Configuration.ConfigurationManager.AppSettings["WebsitePath"];
            string host = qapathSys + "/Account/GetVideo?galleryId=";
            var galleryList = UnitOfWork.VideoGalleryRepository.GetAll(g => g.PetId == petId, navigationProperties: x => x.Pet).Select(g => new IndexViewModel(g, host));

            return PartialView("_Index", galleryList);
        }

        [HttpGet]
        public ActionResult ListVideo(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            var myCountRecord = UnitOfWork.VideoGalleryRepository.GetAll(m => m.IsEncoded == false).Count();
            Session["Count"] = myCountRecord;
            Uri uri = System.Web.HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetVideo?galleryId=";
            var galleryList = UnitOfWork.VideoGalleryRepository.GetAll(g => g.PetId == petId, navigationProperties: x => x.Pet).Select(g => new IndexViewModel(g, host));

            return PartialView("_ListVideo", galleryList);
        }

        [HttpGet]
        public ActionResult AddVideo(int petId)
        {
            return PartialView("_AddVideo", new AddViewModel(petId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVideo(HttpPostedFileBase filebase, FormCollection fc)
        {
            var userId = HttpContext.User.GetUserId();

            AddViewModel model = new AddViewModel();

            model.Title = fc["Title"];
            model.PetId = Convert.ToInt32(fc["PetId"]);

            if (ModelState.IsValid)
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    try
                    {
                        imgFile = Request.Files["VideoURL"];
                        fileName = Path.Combine(imgFile.FileName);
                        string fileextension = Path.GetExtension(fileName);
                        virtaulName = DateTime.Now.ToString("yyyymmddhhmmss") + fileName.Replace(" ", "").Trim();
                        testName = virtaulName.Replace("&", "").Trim();
                        string directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(HttpContext.User.GetInfoPath(), userId.ToString()), Constants.GalleryVideoFolderName);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        path = Path.Combine(directoryPath, testName);
                        imgFile.SaveAs(path);
                        string DummyPath = Path.Combine("\\Content\\images\\DummyVideo.jpg");
                        model.VideoURL = path;
                        model.VideoImage = DummyPath;
                        Model.VideoGallery videoGalleryModel = model.Map();
                        UnitOfWork.VideoGalleryRepository.Insert(videoGalleryModel);
                        UnitOfWork.Save();
                        Success(Resources.Wording.Gallery_Add_AddVideoSuccessMessage);
                        LaunchEncoding(videoGalleryModel.Id);
                        imgOutputpath = Path.Combine("\\Content\\myimages", demopath);
                        Outputpath = Path.Combine(("\\Content\\Video"), outputVideoName);
                    }
                    catch (Exception ex) { throw new Exception("Exception :: " + ex.StackTrace); }
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
        public ActionResult EditVideo(int VideoId)
        {
            var gallery = UnitOfWork.VideoGalleryRepository.GetSingle(g => g.Id == VideoId);
            return PartialView("_EditVideo", new EditViewModel(gallery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVideo(EditViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var gallery = UnitOfWork.VideoGalleryRepository.GetSingleTracking(g => g.Id == model.Id);
                model.Map(gallery);

                UnitOfWork.VideoGalleryRepository.Update(gallery);
                UnitOfWork.Save();
                Success(Resources.Wording.Gallery_Edit_UpdateVideoSuccessMessage);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteVideo(int VideoId)
        {
            var gallery = UnitOfWork.VideoGalleryRepository.GetSingle(g => g.Id == VideoId);
            return PartialView("_DeleteVideo", new DeleteViewModel(gallery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVideo(int id, int petId)
        {
            var gallery = UnitOfWork.VideoGalleryRepository.GetSingle(c => c.Id == id);
            var myvideourl = UnitOfWork.VideoGalleryRepository.GetSingle(c => c.Id == id).VideoURL;
            var myvideoImage = UnitOfWork.VideoGalleryRepository.GetSingle(c => c.Id == id).VideoImage;
            string fullPath = Request.MapPath("" + myvideourl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            string FullPathImage = Request.MapPath("" + myvideoImage);
            if (System.IO.File.Exists(FullPathImage))
            {
                System.IO.File.Delete(FullPathImage);
            }

            UnitOfWork.VideoGalleryRepository.Delete(gallery);
            UnitOfWork.Save();
            Success(Resources.Wording.Gallery_Delete_DeleteVideoSuccessMessage);
            return RedirectToAction("Index", new { petId = petId });
        }

        [HttpGet]
        public ActionResult VideoDetails(int videoId, int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            ViewBag.Likes = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == videoId && x.GalleryType == 3).Count().ToString();
            ViewBag.Comments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == videoId && x.GalleryType == 3).Count().ToString();
            var glist = UnitOfWork.VideoGalleryRepository.GetAll(x => x.PetId == petId).ToList();
           // Uri uri = System.Web.HttpContext.Current.Request.Url;
            string qapathSys = System.Configuration.ConfigurationManager.AppSettings["WebsitePath"];
           // string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetVideo?galleryId=";
            string host = qapathSys + "/Account/GetVideo?galleryId=";
            ViewBag.Last = glist.LastOrDefault() == null ? false : glist.LastOrDefault().Id == videoId ? true : false;
            ViewBag.First = glist.FirstOrDefault() == null ? false : glist.FirstOrDefault().Id == videoId ? true : false;
            var galleryList = UnitOfWork.VideoGalleryRepository.GetAll(g => g.Id == videoId && g.PetId == petId, navigationProperties: c => c.Pet).Select(g => new IndexViewModel(g, host)).FirstOrDefault();
            if (galleryList == null)
            {
                return RedirectToAction("Index", "Pet");
            }
            return View("VideoDetails", galleryList);
        }

        [HttpGet]
        public ActionResult PrevNextImage(int videoId, int petId, string type)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ViewBag.PetId = petId;
            int? Imgid = 0;
            bool flag = false;
            var glist = UnitOfWork.VideoGalleryRepository.GetAll(x => x.PetId == petId).ToList();
            foreach (var gallery in glist)
            {
                if (flag)
                {
                    Imgid = gallery.Id;
                    break;
                }
                if (gallery.Id == videoId && type == "next")
                {
                    flag = true;
                }
                if (gallery.Id == videoId && type == "prev")
                {
                    break;
                }
                Imgid = gallery.Id;
            }

            ViewBag.Last = glist.LastOrDefault() == null ? false : glist.LastOrDefault().Id == Imgid ? true : false;
            ViewBag.First = glist.FirstOrDefault() == null ? false : glist.FirstOrDefault().Id == Imgid ? true : false;
            Uri uri = System.Web.HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + "/Account/GetVideo?galleryId=";
            ViewBag.Likes = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == Imgid && x.GalleryType == 3).Count().ToString();
            ViewBag.Comments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == Imgid && x.GalleryType == 3).Count().ToString();
            var galleryList = UnitOfWork.VideoGalleryRepository.GetAll(g => g.Id == Imgid && g.PetId == petId, navigationProperties: c => c.Pet).Select(g => new IndexViewModel(g, host)).FirstOrDefault();
            if (galleryList == null)
            {
                return RedirectToAction("Index", "Pet");
            }
            return PartialView("_Details", galleryList);
        }

        [HttpGet]
        public ActionResult GetVideoImage(int galleryId)
        {
            var gal = UnitOfWork.VideoGalleryRepository.GetSingle(x => x.Id == galleryId);

            Uri uri = System.Web.HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Authority;
            String RelativePath = Server.MapPath(gal.VideoImage.Trim().Replace("\\", "/"));
            var fs = new FileStream(RelativePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return new FileStreamResult(fs, "image/*");
        }

        #region Private Methods

        private string GetVideoUrlId(string videoUrl)
        {
            string desiredValue = "";
            try
            {
                if (videoUrl.Contains("/v/"))
                {
                    int mInd = videoUrl.IndexOf("/v/");
                    if (mInd != -1)
                    {
                        string strVideoCode = videoUrl.Substring(videoUrl.IndexOf("/v/") + 3);
                        int ind = strVideoCode.IndexOf("?");
                        strVideoCode = strVideoCode.Substring(0, ind == -1 ? strVideoCode.Length : ind);
                        desiredValue = strVideoCode;
                    }
                }
                else
                {
                    string[] parts = videoUrl.Replace('?', ' ').Split('=');
                    if (parts.Count() > 0)
                    {
                        desiredValue = parts[1];
                    }
                }
            }
            catch { }
            return desiredValue;
        }

        private void LaunchEncoding(int videoId)
        {
            
            outputVideoName = testName.Substring(0, testName.LastIndexOf(".")) + ".mp4"; ;
            inputVideo = path;
            demopath = Path.ChangeExtension(outputVideoName, "png");
            string pathhig = Path.Combine(Server.MapPath("~\\Content\\"),Constants.Video);
            if (!Directory.Exists(pathhig))
            {
                Directory.CreateDirectory(pathhig);
            }
            pathhigh = (pathhig +"\\"+ outputVideoName);

            string pathimage = Path.Combine(Server.MapPath("~\\Content\\"), Constants.myimages);
            if (!Directory.Exists(pathimage))
            {
                Directory.CreateDirectory(pathimage);
            }

            mypath = (pathimage + "\\" + demopath);
            if (!fileName.ToLower().EndsWith(".mov") || !fileName.ToLower().EndsWith(".mp4"))
            {
                argsHigh =
                   @" -c:v libx264 -crf 22 -ac 2 -ar 44100 -y ";

                argsScreen =
                    @" -ss 00:00:1.435 -vframes 1 -y ";
            }
            else
            {
                exiftool = new Process();

                exiftool.StartInfo.Arguments = " " + inputVideo;
                exiftool.StartInfo.FileName = Server.MapPath("~\\bin\\exiftool.exe");
                exiftool.StartInfo.CreateNoWindow = true;
                exiftool.StartInfo.UseShellExecute = false;
                exiftool.EnableRaisingEvents = false;
                exiftool.StartInfo.RedirectStandardOutput = true;
                exiftool.StartInfo.CreateNoWindow = true;
                exiftool.StartInfo.RedirectStandardError = true;
                exiftool.StartInfo.LoadUserProfile = true;
                exiftool.Start();

                string meta = exiftool.StandardOutput.ReadToEnd();
                string[] lstMeta = meta.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                argsHigh =
                   @" -vcodec copy -acodec copy -y ";

                argsScreen =
                    @" -ss 00:00:1.435 -vframes 1 -y ";

                foreach (string data in lstMeta)
                {
                    if (data.Contains("Rotation"))
                    {
                        if (data.EndsWith(("90")))
                        {
                            argsHigh =
                                 @" -vf ""rotate=90*PI/180"" -acodec copy -metadata:s:v rotate=""0"" -y ";

                            argsScreen =
                                @" -vf ""rotate=90*PI/180"" -ss 00:00:1.435 -vframes 1 -y ";
                            break;
                        }
                        else if (data.EndsWith(("180")))
                        {
                            argsHigh =
                               @" -vf ""rotate=180*PI/180"" -acodec copy -metadata:s:v rotate=""0"" -y ";

                            argsScreen =
                                @" -vf ""rotate=180*PI/180"" -ss 00:00:1.435 -vframes 1 -y ";
                            break;
                        }
                        else if (data.EndsWith(("270")))
                        {
                            argsHigh =
                               @" -vf ""rotate=270*PI/180"" -acodec copy -metadata:s:v rotate=""0"" -y ";

                            argsScreen =
                                @" -vf ""rotate=270*PI/180"" -ss 00:00:1.435 -vframes 1 -y ";
                            break;
                        }

                    }
                    else if (data.Contains("MIME Type"))
                    {
                        if (data.EndsWith("quicktime"))
                        {
                            argsHigh =
                               @" -an -vcodec libx264 -crf 23 -y ";

                            argsScreen =
                                @" -ss 00:00:1.435 -vframes 1 -y ";
                            break;
                        }

                    }
                }
                while (!exiftool.HasExited)
                {

                }
            }

            ffmpeg = new Process();
            ffmpeg.StartInfo.Arguments = " -threads 0 -i " + inputVideo + argsHigh + pathhigh;
            ffmpeg.StartInfo.FileName = Server.MapPath("~\\bin\\ffmpeg.exe");
            ffmpeg.StartInfo.CreateNoWindow = true;
            ffmpeg.StartInfo.UseShellExecute = false;
            ffmpeg.EnableRaisingEvents = true;
            ffmpeg.ErrorDataReceived += ffmpeglow_ErrorDataReceived;
            ffmpeg.Exited += new EventHandler((s, e) => ffmpeg_Exited(s, e, videoId));

            ffmpeg.Start();


            ffmpegScreen = new Process();
            ffmpegScreen.StartInfo.Arguments = " -threads 0 -i " + inputVideo + argsScreen + mypath;
            ffmpegScreen.StartInfo.FileName = Server.MapPath("~\\bin\\ffmpeg.exe");
            ffmpegScreen.StartInfo.CreateNoWindow = true;
            ffmpegScreen.StartInfo.UseShellExecute = false;
            ffmpegScreen.EnableRaisingEvents = true;
            ffmpegScreen.ErrorDataReceived += ffmpegScreen_ErrorDataReceived;
            ffmpegScreen.Exited += new EventHandler((s, e) => ffmpegScreen_Exited(s, e, videoId));

            ffmpegScreen.Start();

        }

        void ffmpeglow_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
        }

        private void ffmpeg_Exited(object sender, EventArgs e, int videoId)
        {
            highDone = true;

            ffmpeg.Dispose();
            ffmpeg.Close();

            AllProcessDone(videoId);
        }

        private void AllProcessDone(int videoId)
        {
            if (highDone && screenDone)
            {
                System.IO.File.Delete(path);

                if (videoId != 0)
                {
                    UnitOfWork UnitOfWork = new UnitOfWork();
                    var gallery = UnitOfWork.VideoGalleryRepository.GetSingleTracking(g => g.Id == videoId);
                    gallery.IsEncoded = true;
                    gallery.VideoURL = Outputpath;
                    gallery.VideoImage = imgOutputpath;
                    UnitOfWork.VideoGalleryRepository.Update(gallery);
                    UnitOfWork.Save();
                    UnitOfWork.Dispose();
                }
            }
        }

        void ffmpegScreen_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
        }

        private void ffmpegScreen_Exited(object sender, System.EventArgs e, int videoId)
        {
            screenDone = true;

            ffmpegScreen.Dispose();
            ffmpegScreen.Close();

            AllProcessDone(videoId);
        }

        #endregion
    }

}





