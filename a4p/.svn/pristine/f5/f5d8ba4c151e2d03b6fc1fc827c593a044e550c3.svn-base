using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using ADOPets.Web.Common.Helpers;
using System.Web.Mvc;

namespace ADOPets.Web.ViewModels.PhotoGallery
{
    public class IndexViewModel
    {
        //private Gallery a;

        public IndexViewModel() { }

        public IndexViewModel(Model.Gallery gallery, byte[] imagePath)
        {
            ImageURL = gallery.ImageURL;
            Title = gallery.Title;
            ImageId = gallery.Id;
            CreationDate = Convert.ToDateTime(gallery.CreatedDate);
            ImagePath = imagePath;
            PetId = gallery.PetId;
            PetName = gallery.Pet.Name;
        }
        public IndexViewModel(Gallery gallery, string url)
        {
            Title = gallery.Title;
            ImageId = gallery.Id;
            CreationDate = Convert.ToDateTime(gallery.CreatedDate);
            ImageURL = gallery.ImageURL;
            PetId = gallery.PetId;
            PetName = gallery.Pet.Name;
            CompleteImageURL = url + gallery.Id.ToString();
        }

        public IndexViewModel(Gallery gallery,string url,int albumid)
        {
            Title = gallery.Title;
            ImageId = gallery.Id;
            CreationDate = Convert.ToDateTime(gallery.CreatedDate);
            ImageURL = gallery.ImageURL;
            PetId = gallery.PetId;
            PetName = gallery.Pet.Name;
            CompleteImageURL = url + gallery.Id.ToString();
            AlbumId = albumid;
        }

        public IndexViewModel(Gallery gallery, int albumid)
        {
            Title = gallery.Title;
            ImageId = gallery.Id;
            CreationDate = Convert.ToDateTime(gallery.CreatedDate);
            ImageURL = gallery.ImageURL;
            PetId = gallery.PetId;
            PetName = gallery.Pet.Name;
            AlbumId = albumid;
        }


        public byte[] ImagePath { get; set; }
        public string ImageURL { get; set; }
        public string CompleteImageURL { get; set; }
        public string Title { get; set; }
        public int ImageId { get; set; }
        public int AlbumId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public DateTime CreationDate { get; set; }


    }
}