using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.PhotoGallery
{
    public class EditViewModel
    {
        public EditViewModel() { }
        public EditViewModel(Model.Gallery gallery)
        {
            Id = gallery.Id;
            Title = gallery.Title;
            ImagePath = gallery.ImageURL;
            PetId = gallery.PetId;
        }

        [Display(Name = "Gallery_Edit_Title", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Gallery_Add_TitleRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Title { get; set; }

        [Display(Name = "Gallery_Add_Image", ResourceType = typeof(Wording))]
        public string ImagePath { get; set; }

        public int Id { get; set; }
        public int PetId { get; set; }

        public void Map(Model.Gallery gallery)
        {
            gallery.Title = new EncryptedText(Title);
            gallery.PetId = PetId;
        }
    }
}