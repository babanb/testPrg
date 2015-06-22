using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.PhotoGallery
{
    public class CommentsViewModel
    {

        public CommentsViewModel()
        { }
        public CommentsViewModel(Model.GalleryComment model)
        {
            CommentId=model.Id;
            GalleryId = model.GalleryId;
            GalleryType = model.GalleryType;
            UserId = model.UserId;
            Comment = model.Comment;
            CreationDate = Convert.ToDateTime(model.CreationDate);
            IsRead = model.IsRead;
            FirstName = model.User.FirstName;
            LastName = model.User.LastName;
            GalleryCommentReplies = model.GalleryCommentReplies;
        }
        public CommentsViewModel(Model.GalleryComment model, List<Model.GalleryCommentReply> galleryCommentReplies)
        {
            CommentId = model.Id;
            GalleryId = model.GalleryId;
            GalleryType = model.GalleryType;
            UserId = model.UserId;
            Comment = model.Comment;
            CreationDate = Convert.ToDateTime(model.CreationDate);
            IsRead = model.IsRead;
            FirstName = model.User.FirstName;
            LastName = model.User.LastName;
            GalleryCommentReplies = galleryCommentReplies;
        }

        public int CommentId { get; set; }
        public string Comment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public int GalleryId { get; set; }
        public int GalleryType { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsRead { get; set; }

        public ICollection<GalleryCommentReply> GalleryCommentReplies { get; set; }
        public User User { get; set; }
    }
}