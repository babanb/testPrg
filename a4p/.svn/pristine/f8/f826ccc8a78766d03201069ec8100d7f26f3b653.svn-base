using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ADOPets.Web.Common;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using Model;
using ADOPets.Web.ViewModels.NewsFeed;
using System.Linq.Expressions;
using System.Globalization;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class NewsFeedController : BaseController
    {

        [HttpGet]
        [ChildActionOnly]//Show Newsfeeds on Layout page
        public ActionResult NewsFeedNotification()
        {
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = GetNewsFeedList();
            var result = lstNewsfeeds.OrderByDescending(x => x.NotificationDate).ToList().Take(4);
            return PartialView("_NewsFeedNotification", result);
        }

        //List the Newsfeed History
        [HttpGet]
        public ActionResult NewsFeedList()
        {
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = GetNewsFeedList();
            List<NewsFeedHistoryModel> result = lstNewsfeeds.OrderByDescending(x => x.NotificationDate)
                                                        .GroupBy(p => p.NotificationDate.Month)
                                                        .Select(g => new NewsFeedHistoryModel(g.OrderByDescending(p => p.NotificationDate).ToList(),
                                                            g.Select(a => a.NotificationDate)))
                                                        .ToList();
            return View("NewsFeedList", result);
        }

        //List the partial view of Newsfeed History
        [HttpGet]
        public ActionResult NewsFeedPartial()
        {
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = GetNewsFeedList();
            List<NewsFeedHistoryModel> result = lstNewsfeeds.OrderByDescending(x => x.NotificationDate)
                                                        .GroupBy(p => p.NotificationDate.Month)
                                                        .Select(g => new NewsFeedHistoryModel(g.OrderByDescending(p => p.NotificationDate).ToList(),
                                                            g.Select(a => a.NotificationDate)))
                                                        .ToList();
            return PartialView("_NewsFeedHistory", result);
        }

        //Search Newsfeeds using StartDate and EndDate
        [HttpGet]
        public ActionResult SearchNotification(string StartDate, string EndDate)
        {
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = GetNewsFeedList();
            DateTime? StartDate1 = (!string.IsNullOrEmpty(StartDate)) ? (DateTime?)Convert.ToDateTime(StartDate, CultureInfo.CurrentCulture) : null;
            DateTime? EndDate1 = (!string.IsNullOrEmpty(EndDate)) ? (DateTime?)Convert.ToDateTime(EndDate, CultureInfo.CurrentCulture) : null;



            if (StartDate1 != null && EndDate1 != null)
            {
                lstNewsfeeds = lstNewsfeeds.Where(n => n.NotificationDate.Date <= EndDate1 && n.NotificationDate >= StartDate1).ToList();
            }
            else if (StartDate1 != null && EndDate1 == null)
            {
                var maxDate = lstNewsfeeds.Select(c => c.NotificationDate).Max();
                lstNewsfeeds = lstNewsfeeds.Where(c => c.NotificationDate >= StartDate1 && c.NotificationDate <= maxDate).ToList();
            }
            else if (StartDate1 == null && EndDate1 != null)
            {
                var minDate = lstNewsfeeds.Select(c => c.NotificationDate).Min();
                lstNewsfeeds = lstNewsfeeds.Where(c => c.NotificationDate >= minDate && Convert.ToDateTime(c.NotificationDate.ToShortDateString()).Date <= EndDate1).ToList();
            }
            else
            {
                lstNewsfeeds = lstNewsfeeds.ToList();
            }


            //if (StartDate != null && EndDate != null)
            //{
            //    lstNewsfeeds = lstNewsfeeds.ToList().Where(n => n.NotificationDate.Date <= EndDate1 && n.NotificationDate >= StartDate1).ToList();
            //}
            List<NewsFeedHistoryModel> result = lstNewsfeeds.OrderByDescending(x => x.NotificationDate)
                                                        .GroupBy(p => p.NotificationDate.Month)
                                                        .Select(g => new NewsFeedHistoryModel(g.OrderByDescending(p => p.NotificationDate).ToList(), g.Select(a => a.NotificationDate)))
                                                        .ToList();

            return PartialView("_NewsFeedHistory", result);
        }

        [HttpGet]
        public ActionResult GetImage(string fromUserId)
        {
            int userId = Convert.ToInt32(fromUserId);
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            if (user == null || user.ProfileImage == null)
            {
                return File("~/Content/Images/ownerProfilepic.jpg", "image/jpg");
            }
            return File(user.ProfileImage, "image/jpg");
        }

        public ActionResult ShowGallery(string galleryId, string type, int albumId)
        {
            int gId = Convert.ToInt32(galleryId);
            if (type.ToLower().Contains("photo"))
            {
                int? petId = UnitOfWork.GalleryRepository.GetSingle(x => x.Id == gId).PetId;
                return RedirectToAction("PhotoDetails", "PhotoGallery", new { imageId = gId, petId = petId });
            }
            if (type.ToLower().Contains("album"))
            {
                if (albumId != 0)
                {
                    int? petId = UnitOfWork.AlbumGalleryRepository.GetSingle(x => x.Id == albumId).PetId;
                    return RedirectToAction("AlbumDetails", "AlbumGallery", new { imageId = gId, petId = petId, albumId = albumId });
                }
            }
            if (type.ToLower().Contains("video"))
            {
                int? petId = UnitOfWork.VideoGalleryRepository.GetSingle(x => x.Id == gId).PetId;
                return RedirectToAction("VideoDetails", "VideoGallery", new { videoId = gId, petId = petId });
            }
            if (type.ToLower() == "commentreply")
            {
                int? petId = 0;
                var comment = UnitOfWork.GalleryCommentRepository.GetSingle(x => x.Id == gId);
                if (comment.GalleryType == 1)
                {
                    petId = UnitOfWork.GalleryRepository.GetSingle(x => x.Id == comment.GalleryId).PetId;
                    return RedirectToAction("PhotoDetails", "PhotoGallery", new { imageId = comment.GalleryId, petId = petId });
                }
                if (comment.GalleryType == 2)
                {
                    petId = UnitOfWork.AlbumGalleryRepository.GetSingle(x => x.Id == comment.AlbumId).PetId;
                    return RedirectToAction("AlbumDetails", "AlbumGallery", new { imageId = comment.GalleryId, petId = petId, albumId = comment.AlbumId });
                }
                if (comment.GalleryType == 3)
                {
                    petId = UnitOfWork.VideoGalleryRepository.GetSingle(x => x.Id == comment.GalleryId).PetId;
                    return RedirectToAction("VideoDetails", "VideoGallery", new { videoId = comment.GalleryId, petId = petId });
                }
            }
            return RedirectToAction("NewsFeedList");
        }

        #region Private methods
        private bool IsShareUser(int contactId, int petId)
        {
            if (UnitOfWork.SharePetInformationRepository.GetAll(x => (x.ContactId == contactId) && x.PetId == petId && x.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery).Count() > 0)
            {
                return true;
            }
            return false;
        }

        private bool IsCommunityUser(int contactId, int PetId)
        {
            if (UnitOfWork.SharePetInfoCommunityRepository.GetAll(x => (x.ContactId == contactId) && x.PetId == PetId && x.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery).Count() > 0)
            {
                return true;
            }
            return false;
        }

        private bool IsOwnerUser(int contactId, int petId)
        {
            var pets = UnitOfWork.PetRepository.GetSingle(x => x.Id == petId, navigationProperties: u => u.Users);
            if (pets != null)
            {
                int count = pets.Users.Where(x => x.Id == contactId).Count();
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private List<NewsFeedNotificationsViewModel> GetNewsFeedList()
        {
            var userId = HttpContext.User.GetUserId();
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = new List<NewsFeedNotificationsViewModel>();
            foreach (NewsFeedNotificationsViewModel obj in GetNewsfeedsForNewlyAddedItems(userId))
            {
                if (IsShareUser(userId, obj.PetId) || IsCommunityUser(userId, obj.PetId) || IsOwnerUser(userId, obj.PetId))
                {
                    lstNewsfeeds.Add(obj);
                }
            }
            foreach (NewsFeedNotificationsViewModel obj in GetNewsfeedsForLikes(userId))
            {
                if (IsShareUser(userId, obj.PetId) || IsCommunityUser(userId, obj.PetId) || IsOwnerUser(userId, obj.PetId))
                {
                    lstNewsfeeds.Add(obj);
                }
            }
            foreach (NewsFeedNotificationsViewModel obj in GetNewsfeedsForComments(userId))
            {
                if (IsShareUser(userId, obj.PetId) || IsCommunityUser(userId, obj.PetId) || IsOwnerUser(userId, obj.PetId))
                {
                    lstNewsfeeds.Add(obj);
                }
            }
            foreach (NewsFeedNotificationsViewModel obj in GetNewsfeedsForReply(userId))
            {
                if (IsShareUser(userId, obj.PetId) || IsCommunityUser(userId, obj.PetId) || IsOwnerUser(userId, obj.PetId))
                {
                    lstNewsfeeds.Add(obj);
                }
            }
            return lstNewsfeeds;
        }

        private List<NewsFeedNotificationsViewModel> GetNewsfeedsForLikes(int userId)
        {
            NewsFeedNotificationsViewModel obj;
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = new List<NewsFeedNotificationsViewModel>();
            var listLikes = UnitOfWork.GalleryLikeRepository.GetAll(navigationProperties: v => v.User);
            foreach (var item in listLikes)
            {
                if (item.GalleryType == 1)
                {
                    var photoGallery = UnitOfWork.GalleryRepository.GetSingle(x => x.Id == item.GalleryId);
                    if (photoGallery != null)
                    {
                        int? petId = photoGallery.PetId;
                        obj = new NewsFeedNotificationsViewModel(item.GalleryId.ToString(), item.User.FirstName + " " + item.User.LastName, "likesphoto", item.CreationDate, item.UserId, @Resources.Wording.NewsFeed_Likes_Photo, petId);
                        lstNewsfeeds.Add(obj);
                    }
                }
                if (item.GalleryType == 2)
                {
                    var albumGallery = UnitOfWork.AlbumGalleryRepository.GetSingle(x => x.Id == item.AlbumId);
                    if (albumGallery != null)
                    {
                        int? petId = albumGallery.PetId;
                        obj = new NewsFeedNotificationsViewModel(item.GalleryId.ToString(), item.User.FirstName + " " + item.User.LastName, "likesalbum", item.CreationDate, item.AlbumId, item.UserId, @Resources.Wording.NewsFeed_Likes_Album, petId);
                        lstNewsfeeds.Add(obj);
                    }
                }
                if (item.GalleryType == 3)
                {
                    var videoGallery = UnitOfWork.VideoGalleryRepository.GetSingle(x => x.Id == item.GalleryId);
                    if (videoGallery != null)
                    {
                        int? petId = videoGallery.PetId;
                        obj = new NewsFeedNotificationsViewModel(item.GalleryId.ToString(), item.User.FirstName + " " + item.User.LastName, "likesvideo", item.CreationDate, item.UserId, @Resources.Wording.NewsFeed_Likes_Video, petId);
                        lstNewsfeeds.Add(obj);
                    }
                }
            }
            return lstNewsfeeds;
        }

        private List<NewsFeedNotificationsViewModel> GetNewsfeedsForComments(int userId)
        {
            NewsFeedNotificationsViewModel obj;
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = new List<NewsFeedNotificationsViewModel>();

            var listComments = UnitOfWork.GalleryCommentRepository.GetAll(navigationProperties: v => v.User);
            foreach (var item in listComments)
            {
                if (item.GalleryType == 1)
                {
                    var photoGallery = UnitOfWork.GalleryRepository.GetSingle(x => x.Id == item.GalleryId);
                    if (photoGallery != null)
                    {
                        int? petId = photoGallery.PetId;
                        obj = new NewsFeedNotificationsViewModel(item.GalleryId.ToString(), item.User.FirstName + " " + item.User.LastName, "commentphoto", item.CreationDate, item.UserId, @Resources.Wording.NewsFeed_Commet_Photo, petId);
                        lstNewsfeeds.Add(obj);
                    }
                }
                if (item.GalleryType == 2)
                {
                    var albumGallery = UnitOfWork.AlbumGalleryRepository.GetSingle(x => x.Id == item.AlbumId);
                    if (albumGallery != null)
                    {
                        int? petId = albumGallery.PetId;
                        obj = new NewsFeedNotificationsViewModel(item.GalleryId.ToString(), item.User.FirstName + " " + item.User.LastName, "commentalbum", item.CreationDate, item.AlbumId, item.UserId, @Resources.Wording.NewsFeed_Commet_Album, petId);
                        lstNewsfeeds.Add(obj);
                    }
                }
                if (item.GalleryType == 3)
                {
                    var videoGallery = UnitOfWork.VideoGalleryRepository.GetSingle(x => x.Id == item.GalleryId);
                    if (videoGallery != null)
                    {
                        int? petId = videoGallery.PetId;
                        obj = new NewsFeedNotificationsViewModel(item.GalleryId.ToString(), item.User.FirstName + " " + item.User.LastName, "commentvideo", item.CreationDate, item.UserId, @Resources.Wording.NewsFeed_Commet_Video, petId);
                        lstNewsfeeds.Add(obj);
                    }
                }
            }
            return lstNewsfeeds;
        }

        //Get for which user which gallery item is added
        private List<NewsFeedNotificationsViewModel> GetNewsfeedsForNewlyAddedItems(int userId)
        {
            //For Photo Gallery
            var PetPhotoUsers = UnitOfWork.GalleryRepository.GetAll(g => !g.IsDeleted && g.IsGalleryPhoto == true, navigationProperties: g => g.Pet).
                Select(x => new NewsFeedNotificationsViewModel(x,
                UnitOfWork.PetRepository.GetSingle(p => p.Id == x.PetId, p => p.Users) == null ? null :
                UnitOfWork.PetRepository.GetSingle(p => p.Id == x.PetId, p => p.Users).Users,
                "photo", Resources.Wording.NewsFeed_Photo, x.PetId)).OrderByDescending(x => x.Id);

            //For Album Gallery
            var PetAlbumUsers = UnitOfWork.GalleryRepository.GetAll(g => !g.IsDeleted && g.IsGalleryPhoto == false, navigationProperties: v => v.AlbumGallery_Photo).Select(x =>
                new NewsFeedNotificationsViewModel(x,
                    UnitOfWork.PetRepository.GetSingle(p => p.Id == x.PetId, p => p.Users) == null ? null :
                    UnitOfWork.PetRepository.GetSingle(p => p.Id == x.PetId, p => p.Users).Users,
                    "album", x.AlbumGallery_Photo.FirstOrDefault() == null ? 0 : x.AlbumGallery_Photo.FirstOrDefault().AlbumId, Resources.Wording.NewsFeed_Album, x.PetId)).OrderByDescending(x => x.Id);

            //For Video Gallery
            var PetVideoUsers = UnitOfWork.VideoGalleryRepository.GetAll(navigationProperties: g => g.Pet).Select(x => new NewsFeedNotificationsViewModel(x,
                UnitOfWork.PetRepository.GetSingle(p => p.Id == x.PetId, p => p.Users) == null ? null :
                UnitOfWork.PetRepository.GetSingle(p => p.Id == x.PetId, p => p.Users).Users,
                "video", Resources.Wording.NewsFeed_Video, x.PetId)).OrderByDescending(x => x.Id);

            List<NewsFeedNotificationsViewModel> lstNewsfeeds = new List<NewsFeedNotificationsViewModel>();
            foreach (NewsFeedNotificationsViewModel item in PetPhotoUsers)
            {
                lstNewsfeeds.Add(item);
            }
            foreach (NewsFeedNotificationsViewModel item in PetAlbumUsers.Where(x => x.AlbumId != 0).ToList())
            {
                lstNewsfeeds.Add(item);
            }
            foreach (NewsFeedNotificationsViewModel item in PetVideoUsers)
            {
                lstNewsfeeds.Add(item);
            }


            return lstNewsfeeds;
        }

        private List<NewsFeedNotificationsViewModel> GetNewsfeedsForReply(int userId)
        {
            NewsFeedNotificationsViewModel obj;
            List<NewsFeedNotificationsViewModel> lstNewsfeeds = new List<NewsFeedNotificationsViewModel>();
            var listReplies = UnitOfWork.GalleryCommentReplyRepository.GetAll(navigationProperties: s => s.User);
            foreach (var item in listReplies)
            {
                int? petId = 0;
                var comment = UnitOfWork.GalleryCommentRepository.GetSingle(x => x.Id == item.CommentId);
                if (comment.GalleryType == 1)
                {
                    var photoGallery = UnitOfWork.GalleryRepository.GetSingle(x => x.Id == comment.GalleryId);
                    if (photoGallery != null)
                    {
                        petId = photoGallery.PetId;
                    }
                }
                if (comment.GalleryType == 2)
                {
                    var albumGallery = UnitOfWork.AlbumGalleryRepository.GetSingle(x => x.Id == comment.AlbumId);
                    if (albumGallery != null)
                    {
                        petId = albumGallery.PetId;
                    }
                }
                if (comment.GalleryType == 3)
                {
                    var videoGallery = UnitOfWork.VideoGalleryRepository.GetSingle(x => x.Id == comment.GalleryId);
                    if (videoGallery != null)
                    {
                        petId = videoGallery.PetId;
                    }
                }
                if (petId != 0)
                {
                    obj = new NewsFeedNotificationsViewModel(item.CommentId.ToString(), item.User.FirstName + " " + item.User.LastName, "commentreply", item.CreationDate, item.UserId, @Resources.Wording.NewsFeed_Reply_Comment, petId);
                    lstNewsfeeds.Add(obj);
                }
            }
            return lstNewsfeeds;
        }

        #endregion
    }
}
