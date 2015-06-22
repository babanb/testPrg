using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.VideoGallery
{
    public class AddViewModel
    {
        public AddViewModel() { }
        public AddViewModel(int petId)
        {
            PetId = petId;
        }

        [Display(Name = "Gallery_Add_Title", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Gallery_Add_TitleRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Title { get; set; }

     //[Url]
        [Display(Name = "Gallery_Add_VideoURL", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Gallery_Add_VideoURLRequired", ErrorMessageResourceType = typeof(Wording))]
       
       
        public string VideoURL { get; set; }

        public int PetId;
        public string VideoImage { get; set; }
        public bool IsEncoded { get; set; }
        

        public Model.VideoGallery Map()
        {
            var videoGallery = new Model.VideoGallery
            {
                Title = new EncryptedText(Title),
                VideoURL = VideoURL,
                PetId = PetId,
                CreatedDate = DateTime.Now,
                VideoImage = VideoImage,
                IsEncoded=IsEncoded
            };
            return videoGallery;
        }
    }
}