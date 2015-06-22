using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.PhotoGallery
{
    public class AddViewModel
    {
        public AddViewModel()
        { }

        public AddViewModel(int petid)
        {
            PetId = petid;
        }

        [Display(Name = "Gallery_Add_Title", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Gallery_Add_TitleRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Title { get; set; }

        [Display(Name = "Gallery_Add_Image", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Gallery_Add_ImageRequired", ErrorMessageResourceType = typeof(Wording))]
        public string ImagePath { get; set; }

        public int PetId;


        public Model.Gallery Map()
        {
            var gallery = new Model.Gallery
            {
                Title = new EncryptedText(Title),
                ImageURL = ImagePath,
                PetId = PetId,
                CreatedDate = DateTime.Now,
                IsGalleryPhoto = true
            };
            return gallery;
        }
    }
}