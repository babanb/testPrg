using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Consultation
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetConsultation consultation)
        {
            Id = consultation.Id;
            ConsultationName = consultation.CustomConsultation;
            VisitDate = consultation.VisitDate;
            Reason = consultation.Reason;
            Comment = consultation.Comment;
        
            PetId = consultation.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Consultation_Edit_ConsultationName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Consultation_Edit_ConsultationNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string ConsultationName { get; set; }

        [Display(Name = "Consultation_Edit_VisitDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Consultation_Edit_VisitDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Consultation_Edit_Reason", ResourceType = typeof(Wording))]
         public string Reason { get; set; }

        [Display(Name = "Consultation_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }


        public int PetId { get; set; }

        public void Map(Model.PetConsultation consultation)
        {
            consultation.CustomConsultation = ConsultationName;
            consultation.VisitDate = VisitDate.Value;
            consultation.Reason = Reason;
            consultation.Comment = new EncryptedText(Comment);
        }
    }
}