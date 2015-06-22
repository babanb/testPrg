using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.AlbumGallery
{
    public class PhotoListViewModel
    {
        public PhotoListViewModel(Model.Gallery gallery, Boolean isAlbumPhoto)
        {
            Title = gallery.Title;
            ImageURL = gallery.ImageURL;
            Id = gallery.Id;
            PetId = gallery.PetId;
            IsAlbumPhoto = isAlbumPhoto;
        }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public int? PetId { get; set; }
        public Boolean IsAlbumPhoto { get; set; }
    }
}