using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Allergy
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetAllergy allergy)
        {
            Id = allergy.Id;
            Allergy = allergy.CustomAllergy;
            StartDate = allergy.StartDate;
            EndDate = allergy.EndDate;
            Reaction = allergy.Reaction;
            Comment = allergy.Comment;
            PetId = allergy.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Allergy_Edit_Allergy", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Allergy_Edit_AllergyRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Allergy { get; set; }

        [Display(Name = "Allergy_Edit_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Allergy_Edit_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Allergy_Edit_Reaction", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Allergy_Edit_ReactionRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reaction { get; set; }

        [Display(Name = "Allergy_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }

        public void Map(Model.PetAllergy allergy)
        {
            allergy.CustomAllergy =Allergy;
            allergy.StartDate = StartDate;
            allergy.EndDate = EndDate;
            allergy.Reaction = Reaction;
            allergy.Comment = new EncryptedText(Comment);
        }
    }
}