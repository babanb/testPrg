using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Hospitalization
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetHospitalization hospitalization)
        {
            Id = hospitalization.Id;
            HospitalName = hospitalization.HospitalName;
            Reason = hospitalization.Reason;
            AdmissionDate = hospitalization.StartDate;
            EndDate = hospitalization.EndDate;
            Comment = hospitalization.Comment;
            UrgentCareVisit = hospitalization.Urgent;
            PetId = hospitalization.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Hospitalization_Delete_HospitalName", ResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Hospitalization_Delete_Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Hospitalization_Delete_AdmissionDate", ResourceType = typeof(Wording))]
        public DateTime? AdmissionDate { get; set; }

        [Display(Name = "Hospitalization_Delete_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Hospitalization_Delete_UrgentCareVisit", ResourceType = typeof(Wording))]
        public bool? UrgentCareVisit { get; set; }

        [Display(Name = "Hospitalization_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }
    }
}