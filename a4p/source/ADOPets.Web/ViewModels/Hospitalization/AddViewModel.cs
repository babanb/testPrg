using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Hospitalization
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

        [Display(Name = "Hospitalization_Add_HospitalName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hospitalization_Add_HospitalNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Hospitalization_Add_Reason", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hospitalization_Add_ReasonRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Hospitalization_Add_AdmissionDate", ResourceType = typeof(Wording))]
        public DateTime? AdmissionDate { get; set; }

        [Display(Name = "Hospitalization_Add_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Hospitalization_Add_UrgentCareVisit", ResourceType = typeof(Wording))]
        public bool UrgentCareVisit { get; set; }

        [Display(Name = "Hospitalization_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public Model.PetHospitalization Map()
        {
            var petHospitalization = new Model.PetHospitalization
            {
                HospitalName = new EncryptedText(HospitalName),
                Reason =Reason,
                StartDate = AdmissionDate.HasValue ? AdmissionDate.Value : (DateTime?)null,
                EndDate = EndDate.HasValue ? EndDate.Value : (DateTime?)null,
                Urgent = UrgentCareVisit,
                Comment = new EncryptedText(Comment),
                PetId = PetId

            };

            return petHospitalization;
        }
    }
}