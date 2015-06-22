using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.AlbumGallery
{
    public class IndexViewModel
    {
        //public IndexViewModel(Model.AlbumGallery albumGallery,Model.Gallery gallery, List<Model.Gallery> lstAlbumGalleryPhoto)
        //{
        //    lstPhoto = lstAlbumGalleryPhoto;
        //    Id = albumGallery.Id;
        //    Title = albumGallery.Title;
        //    ImageURL = gallery.ImageURL;
        //    CreationDate = Convert.ToDateTime(albumGallery.CreatedDate);
        //}


        public IndexViewModel(Model.AlbumGallery albumGallery, string imgURL, int photoCount)
        {
            Id = albumGallery.Id;
            PetId = albumGallery.PetId;
            Title = albumGallery.Title;
            ImageURL = imgURL;
            CreationDate = Convert.ToDateTime(albumGallery.CreatedDate);
            PhotoCount = photoCount;
        }
        public List<Model.Gallery> lstPhoto { get; set; }

        public string ImageURL { get; set; }
        public int Id { get; set; }
        public int? PetId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int PhotoCount { get; set; }
    }
}