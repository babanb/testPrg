using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;


namespace ADOPets.Web.ViewModels.Consultation
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petid)
        {
            PetId = petid;
        }

        public int PetId { get; set; }

        [Display(Name = "Consultation_Add_ConsultationName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Consultation_Add_ConsultationNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string ConsultationName { get; set; }

        [Display(Name = "Consultation_Add_VisitDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Consultation_Add_VisitDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Consultation_Add_Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Consultation_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

     

        public Model.PetConsultation Map()
        {
            var petConsultation = new Model.PetConsultation
            {
                CustomConsultation = ConsultationName,
                VisitDate = VisitDate.Value,
                Reason=Reason,
                Comment = new EncryptedText(Comment),
                PetId = PetId
            };

            return petConsultation;
        }
    }
}