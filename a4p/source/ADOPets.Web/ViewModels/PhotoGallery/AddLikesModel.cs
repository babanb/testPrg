using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.PhotoGallery
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
        }

        public int Id { get; set; }
        public int GalleryId { get; set; }
        public int GalleryType { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<bool> IsRead { get; set; }


        public Model.GalleryLike Map()
        {
            var model = new Model.GalleryLike
            {
                Id = Id,
                GalleryId = GalleryId,
                GalleryType = GalleryType,
                UserId = UserId,
                CreationDate = CreationDate,
                IsRead = IsRead
            };
            return model;
        }
    }
}