using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.VideoGallery
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

       
        public IndexViewModel(Model.VideoGallery videoGallery,string url)
        {
            VideoURL = videoGallery.VideoURL.Trim().Replace("\\","/");
            Title = videoGallery.Title;
            VideoId = videoGallery.Id;
            PetId = videoGallery.PetId;
            PetName = videoGallery.Pet.Name;
            CreationDate = Convert.ToDateTime(videoGallery.CreatedDate);
            VideoImage = videoGallery.VideoImage.Trim().Replace("\\", "/");
            ShareUrl = url + videoGallery.Id;
            IsEncoded = videoGallery.IsEncoded;
        }

        public string VideoURL { get; set; }
        public string ShareUrl { get; set; }
        public string Title { get; set; }
        public int VideoId { get; set; }
        public int? PetId { get; set; }
        public string PetName { get; set; }
        public DateTime CreationDate { get; set; }
        public string VideoImage { get; set; }

        public bool IsEncoded { get; set; }
    }
}