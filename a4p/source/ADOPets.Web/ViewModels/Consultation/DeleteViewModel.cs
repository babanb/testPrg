using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Consultation
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetConsultation consultation)
        {
            Id = consultation.Id;
            ConsultationName = consultation.CustomConsultation;
            VisitDate = consultation.VisitDate;
            Reason = consultation.Reason;
            Comment = consultation.Comment;
            PetId = consultation.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Consultation_Delete_ConsultationName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Consultation_Delete_ConsultationNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string ConsultationName { get; set; }

        [Display(Name = "Consultation_Delete_VisitDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Consultation_Delete_VisitDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Consultation_Delete_Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Consultation_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }


        public int PetId { get; set; }
    }
}