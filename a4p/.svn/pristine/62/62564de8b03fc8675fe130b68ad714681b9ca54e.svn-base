using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Hospitalization
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetHospitalization hospitalization)
        {
            Id = hospitalization.Id;
            HospitalName = hospitalization.HospitalName;
            Reason = hospitalization.Reason;
            AdmissionDate = hospitalization.StartDate;
            EndDate = hospitalization.EndDate;
            UrgentCareVisit = hospitalization.Urgent.HasValue ? hospitalization.Urgent.Value:false;
            Comment= hospitalization.Comment;
            PetId = hospitalization.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Hospitalization_Edit_HospitalName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hospitalization_Edit_HospitalNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Hospitalization_Edit_Reason", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hospitalization_Edit_ReasonRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Hospitalization_Edit_AdmissionDate", ResourceType = typeof(Wording))]
        public DateTime? AdmissionDate { get; set; }

        [Display(Name = "Hospitalization_Edit_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Hospitalization_Edit_UrgentCareVisit", ResourceType = typeof(Wording))]
        public bool UrgentCareVisit { get; set; }

        [Display(Name = "Hospitalization_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }


        public void Map(Model.PetHospitalization hospitalization)
        {
            hospitalization.HospitalName = new EncryptedText(HospitalName);
            hospitalization.Reason = Reason;
            hospitalization.StartDate = AdmissionDate;
            hospitalization.EndDate = EndDate;
            hospitalization.Urgent = UrgentCareVisit;
            hospitalization.Comment = new EncryptedText(Comment);
        }
    }
}