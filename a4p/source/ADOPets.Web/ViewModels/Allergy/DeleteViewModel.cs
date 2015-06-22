using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Allergy
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetAllergy allergy)
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

        [Display(Name = "Allergy_Delete_Allergy", ResourceType = typeof(Wording))]
        public string Allergy { get; set; }

        [Display(Name = "Allergy_Delete_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Allergy_Delete_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Allergy_Delete_Reaction", ResourceType = typeof(Wording))]
        public string Reaction { get; set; }

        [Display(Name = "Allergy_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }
    }
}