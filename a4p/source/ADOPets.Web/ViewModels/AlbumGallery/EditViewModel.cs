using ADOPets.Web.Resources;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADOPets.Web.ViewModels.AlbumGallery
{
    public class EditViewModel
    {
        public EditViewModel() { }
        public EditViewModel(Model.AlbumGallery albumGallery, List<Gallery> lstGalleryPhoto, string isDefault)
        {
            Id = albumGallery.Id;
            Title = albumGallery.Title;         
            PetId = albumGallery.PetId;
            lstAlbumGallery = albumGallery.AlbumGallery_Photo;
            lstGallery = lstGalleryPhoto;
            IsDefaultCover = isDefault;
        }

        [Display(Name = "AlbumGallery_Edit_Title", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "AlbumGallery_Edit_TitleRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Title { get; set; }

        public ICollection<AlbumGallery_Photo> lstAlbumGallery { get; set; }
        public List<Gallery> lstGallery { get; set; }

        public string IsDefaultCover { get; set; }
        public int Id { get; set; }
        public int? PetId { get; set; }

        public void Map(Model.AlbumGallery albumGallery)
        {
            albumGallery.Title = new EncryptedText(Title);
            albumGallery.PetId =Convert.ToInt32(PetId);
            albumGallery.Id = Id;
        }
    }
}