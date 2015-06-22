using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Surgery
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetSurgery surgery)
        {
            Id = surgery.Id;
            Surgery = surgery.CustomSurgery;
            DateofSurgery = surgery.SurgeryDate;
            Reason = surgery.Reason;
            Physician = surgery.Physician;
            Comments = surgery.Comment;
            PetId = surgery.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Surgery_Edit_Surgery", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Surgery_Edit_SurgeryRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Surgery { get; set; }

        [Display(Name = "Surgery_Edit_DateofSurgery", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Surgery_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? DateofSurgery { get; set; }

        [Display(Name = "Surgery_Edit_Reason", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Surgery_Edit_ReasonRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Surgery_Edit_Physician", ResourceType = typeof(Wording))]
        public string Physician { get; set; }

        [Display(Name = "Surgery_Edit_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }


        public int PetId { get; set; }

        public void Map(Model.PetSurgery surgery)
        {
            surgery.CustomSurgery = Surgery;
            surgery.SurgeryDate = DateofSurgery;
            surgery.Reason = Reason;
            surgery.Physician = new EncryptedText(Physician);
            surgery.Comment = new EncryptedText(Comments);

        }
    }
}