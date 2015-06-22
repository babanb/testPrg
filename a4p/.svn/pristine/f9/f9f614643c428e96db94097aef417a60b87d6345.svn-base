using System;
using ADOPets.Web.Resources;
using Model;
using System.ComponentModel.DataAnnotations;

namespace ADOPets.Web.ViewModels.VideoGallery
{
    public class EditViewModel
    {
        public EditViewModel() { }
        public EditViewModel(Model.VideoGallery videoGallery)
        {
            Id = videoGallery.Id;
            Title = videoGallery.Title;
            VideoURL = videoGallery.VideoURL;
            PetId = Convert.ToInt32(videoGallery.PetId);
        }

        [Display(Name = "Gallery_Edit_Title", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Gallery_Edit_TitleRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Title { get; set; }

        [Display(Name = "Gallery_Edit_VideoURL", ResourceType = typeof(Wording))]
        public string VideoURL { get; set; }

        public int Id { get; set; }
        public int PetId { get; set; }
        public string VideoImage { get; set; }

        public void Map(Model.VideoGallery videoGallery)
        {
            videoGallery.Title = new EncryptedText(Title);
            videoGallery.PetId = PetId;
        }
    }
}