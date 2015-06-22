using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Immunization
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

        [Display(Name = "Immunization_Add_Immunization", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Immunization_Add_ImmunizationRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Immunization { get; set; }

        [Display(Name = "Immunization_Add_InjectionDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Immunization_Add_InjectionDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? InjectionDate { get; set; }

        [Display(Name = "Immunization_Add_HospitalName", ResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Immunization_Add_SerialNumber", ResourceType = typeof(Wording))]
        public string SerialNumber { get; set; }

        [Display(Name = "Immunization_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public Model.PetVaccination Map()
        {
            var petImmunization = new Model.PetVaccination
            {
                CustomVaccination = Immunization,
                InjectionDate = InjectionDate.Value,
                HospitalName = new EncryptedText(HospitalName),
                SerialNumber = SerialNumber,
                Comment = new EncryptedText(Comment),
                PetId = PetId
            };

            return petImmunization;
        }
    }
}