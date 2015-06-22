using ADOPets.Web.Resources;
using Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADOPets.Web.ViewModels.AlbumGallery
{
    public class AddViewModel
    {
        public AddViewModel() { }
        public AddViewModel(int petId)
        {
            PetId = petId;
        }

        [Display(Name = "AlbumGallery_Add_Title", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "AlbumGallery_Add_TitleRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Title { get; set; }

        public List<Gallery> lstGallery { get; set; }

        public int PetId { get; set; }
        public string IsDefault { get; set; }
    }
}