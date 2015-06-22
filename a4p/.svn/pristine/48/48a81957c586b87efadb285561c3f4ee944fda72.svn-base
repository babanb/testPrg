using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.GalleryComment
{
    public class AddLikesModel
    {
        public AddLikesModel()
        { }
        public AddLikesModel(Model.GalleryLike model)
        {
            Id = model.Id;
            GalleryId = model.GalleryId;
            GalleryType = model.GalleryType;
            UserId = model.UserId;
            CreationDate = model.CreationDate;
            IsRead = model.IsRead;
            AlbumId = Convert.ToInt32(model.AlbumId);
        }

        public int Id { get; set; }
        public int GalleryId { get; set; }
        public int GalleryType { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public int AlbumId { get; set; }

        public Model.GalleryLike Map()
        {
            var model = new Model.GalleryLike
            {
                Id = Id,
                GalleryId = GalleryId,
                GalleryType = GalleryType,
                UserId = UserId,
                CreationDate = CreationDate,
                IsRead = IsRead,
                AlbumId=AlbumId
            };
            return model;
        }
    }
}