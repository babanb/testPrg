using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.GalleryComment;
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

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class GalleryCommentController : BaseController
    {
        [HttpPost]
        public ActionResult AddLikes(int id, int type,int albumId=0)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            GalleryLike likes = null;
            if (type == 3)
            {
                likes = UnitOfWork.GalleryLikeRepository.GetSingle(x => x.GalleryId == id && x.GalleryType == 3 && x.UserId == userId);
            }
            else
            {
                likes = UnitOfWork.GalleryLikeRepository.GetSingle(x => x.GalleryId == id && (x.GalleryType == 1 || x.GalleryType == 2) && x.UserId == userId);
            }
            if (likes == null)
            {
                AddLikesModel objCommunity = new AddLikesModel();
                objCommunity.GalleryId = id;
                objCommunity.GalleryType = type;
                objCommunity.UserId = userId;
                objCommunity.IsRead = false;
                objCommunity.CreationDate = DateTime.Now;
                objCommunity.AlbumId = albumId;
                objCommunity.Id = 0;
                UnitOfWork.GalleryLikeRepository.Insert(objCommunity.Map());
                UnitOfWork.Save();
            }
            else
            {
                UnitOfWork.GalleryLikeRepository.Delete(likes);
                UnitOfWork.Save();
            }
            string likeCount = "0";
            if (type == 3)
            {
                likeCount = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == id && x.GalleryType == 3).Count().ToString();
            }
            else
            {
                likeCount = UnitOfWork.GalleryLikeRepository.GetAll(x => x.GalleryId == id && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
            }
            return Content(likeCount);
        }

        [HttpPost]
        public ActionResult AddComments(int imageId, string comment, int type,int albumId=0)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            if (!string.IsNullOrEmpty(comment))
            {
                GalleryComment objCommunity = new GalleryComment();
                objCommunity.GalleryId = imageId;
                objCommunity.GalleryType = type;
                objCommunity.UserId = userId;
                objCommunity.Comment = comment;
                objCommunity.IsRead = false;
                objCommunity.AlbumId = albumId;
                objCommunity.CreationDate = DateTime.Now;
                objCommunity.Id = 0;
                UnitOfWork.GalleryCommentRepository.Insert(objCommunity);
                UnitOfWork.Save();
            }

            var lstComments = GetGalleryComments(imageId, type);
            return PartialView("_CommentList", lstComments);
        }

        private IOrderedEnumerable<CommentsViewModel> GetGalleryComments(int imageId, int type)
        {
            IOrderedEnumerable<CommentsViewModel> lstComments = null;
            List<Model.GalleryCommentReply> galleryCommentReplies = UnitOfWork.GalleryCommentReplyRepository.GetAll(navigationProperties: c => c.User).ToList();
            Expression<Func<GalleryComment, object>> parames1 = v => v.User;
            Expression<Func<GalleryComment, object>> parames2 = v => v.GalleryCommentReplies;
            Expression<Func<GalleryComment, object>>[] paramesArray = new Expression<Func<GalleryComment, object>>[] { parames1, parames2 };
            if (type == 3)
            {
                lstComments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == imageId && x.GalleryType == 3, navigationProperties: paramesArray).Select(x => new CommentsViewModel(x, galleryCommentReplies.Where(z => z.CommentId == x.Id).ToList())).OrderByDescending(x => x.CreationDate);
            }
            else
            {
                lstComments = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == imageId && (x.GalleryType == 1 || x.GalleryType == 2), navigationProperties: paramesArray).Select(x => new CommentsViewModel(x, galleryCommentReplies.Where(z => z.CommentId == x.Id).ToList())).OrderByDescending(x => x.CreationDate);
            }
            return lstComments;
        }

        [HttpPost]
        public ActionResult EditComments(int commentId, string comment, int type)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            GalleryComment objCommunity = UnitOfWork.GalleryCommentRepository.GetSingle(x => x.Id == commentId);
            if (!string.IsNullOrEmpty(comment))
            {
                objCommunity.Comment = comment;
                objCommunity.CreationDate = DateTime.Now;
                UnitOfWork.GalleryCommentRepository.Update(objCommunity);
                UnitOfWork.Save();
            }
            var lstComments = GetGalleryComments(objCommunity.GalleryId, objCommunity.GalleryType);
            return PartialView("_CommentList", lstComments);
        }

        [ChildActionOnly]
        public ActionResult ShowComments(int imageId, string type)
        {
            int gtype = Convert.ToInt32(type);
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;

            var lstComments = GetGalleryComments(imageId, gtype);
            return PartialView("_CommentList", lstComments);
        }

        [HttpGet]
        public ActionResult DeleteComment(int commentId, int type)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            GalleryComment objCommunity = UnitOfWork.GalleryCommentRepository.GetSingle(x => x.Id == commentId);
            CommentDeleteModel model = new CommentDeleteModel();
            model.GalleryId = objCommunity.GalleryId;
            model.CommentId = commentId;
            model.GalleryType = type;
            return PartialView("_DeleteComments", model);
        }

        [HttpPost]
        public ActionResult DeleteComment(CommentDeleteModel model)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;

            List<GalleryCommentReply> lstReplies = UnitOfWork.GalleryCommentReplyRepository.GetAll(x => x.CommentId == model.CommentId).ToList();
            foreach (GalleryCommentReply item in lstReplies)
            {
                UnitOfWork.GalleryCommentReplyRepository.Delete(item);
                UnitOfWork.Save();
            }

            GalleryComment objCommunity = UnitOfWork.GalleryCommentRepository.GetSingle(x => x.Id == model.CommentId);
            UnitOfWork.GalleryCommentRepository.Delete(objCommunity);
            UnitOfWork.Save();

            var lstComments = GetGalleryComments(objCommunity.GalleryId, objCommunity.GalleryType);
            return PartialView("_CommentList", lstComments);
        }

        [HttpPost]
        public ActionResult ReplyComments(int commentId, string comment, int type)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            if (!string.IsNullOrEmpty(comment))
            {
                GalleryCommentReply objCommunity = new GalleryCommentReply();
                objCommunity.CommentId = commentId;
                objCommunity.UserId = userId;
                objCommunity.Comment = comment;
                objCommunity.IsRead = false;
                objCommunity.CreationDate = DateTime.Now;
                objCommunity.Id = 0;
                UnitOfWork.GalleryCommentReplyRepository.Insert(objCommunity);
                UnitOfWork.Save();
            }
            int galleryId = UnitOfWork.GalleryCommentRepository.GetSingle(x => x.Id == commentId).GalleryId;
            var lstComments = GetGalleryComments(galleryId, type);
            return PartialView("_CommentList", lstComments);
        }

        [HttpGet]
        public ActionResult GetCommentsCount(int galleryId, int type)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            string commentCount = "0";
            if (type == 3)
            {
                commentCount = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == galleryId && x.GalleryType == 3).Count().ToString();
            }
            else
            {
                commentCount = UnitOfWork.GalleryCommentRepository.GetAll(x => x.GalleryId == galleryId && (x.GalleryType == 1 || x.GalleryType == 2)).Count().ToString();
            }
            return Json(commentCount, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteReply(int replyId, int type, int galleryId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            ReplyDeleteModel model = new ReplyDeleteModel();
            model.ReplyId = replyId;
            model.GalleryType = type;
            model.GalleryId = galleryId;
            return PartialView("_DeleteReply", model);
        }

        [HttpPost]
        public ActionResult DeleteReply(ReplyDeleteModel model)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            GalleryCommentReply lstReplies = UnitOfWork.GalleryCommentReplyRepository.GetSingle(x => x.Id == model.ReplyId);
            UnitOfWork.GalleryCommentReplyRepository.Delete(lstReplies);
            UnitOfWork.Save();
            var lstComments = GetGalleryComments(model.GalleryId, model.GalleryType);
            return PartialView("_CommentList", lstComments);
        }

        [HttpPost]
        public ActionResult EditReply(int replyid, string comment, int type, int galleryId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            if (!string.IsNullOrEmpty(comment))
            {
                GalleryCommentReply lstReplies = UnitOfWork.GalleryCommentReplyRepository.GetSingle(x => x.Id == replyid);
                lstReplies.Comment = comment;
                lstReplies.CreationDate = DateTime.Now;
                UnitOfWork.GalleryCommentReplyRepository.Update(lstReplies);
                UnitOfWork.Save();
            }
            var lstComments = GetGalleryComments(galleryId, type);
            return PartialView("_CommentList", lstComments);
        }
    }
}
