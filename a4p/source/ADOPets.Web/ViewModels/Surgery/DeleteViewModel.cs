using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Surgery
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetSurgery surgery)
        {
            Id = surgery.Id;
            Surgery = surgery.CustomSurgery;
            DateOfSurgery = surgery.SurgeryDate;
            Reason = surgery.Reason;
            Physician = surgery.Physician;
            Comments = surgery.Comment;
            PetId = surgery.PetId;

        }

        public int Id { get; set; }

        [Display(Name = "Surgery_Delete_Surgery", ResourceType = typeof(Wording))]
        public string Surgery { get; set; }

        [Display(Name = "Surgery_Delete_DateofSurgery", ResourceType = typeof(Wording))]
        public DateTime? DateOfSurgery { get; set; }

        [Display(Name = "Surgery_Delete_Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Surgery_Delete_Physician", ResourceType = typeof(Wording))]
        public string Physician { get; set; }

        [Display(Name = "Surgery_Delete_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public int PetId { get; set; }

    }
}
