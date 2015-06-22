using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Immunization
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetVaccination immunization)
        {
            Id = immunization.Id;
            Immunization = immunization.CustomVaccination;
            InjectionDate = immunization.InjectionDate;
            HospitalName = immunization.HospitalName;
            SerialNumber = immunization.SerialNumber;
            Comment= immunization.Comment;
            PetId = immunization.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Immunization_Edit_Immunization", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Immunization_Edit_ImmunizationRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Immunization { get; set; }

        [Display(Name = "Immunization_Edit_InjectionDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Immunization_Edit_InjectionDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime InjectionDate { get; set; }

        [Display(Name = "Immunization_Edit_HospitalName", ResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Immunization_Edit_SerialNumber", ResourceType = typeof(Wording))]
        public string SerialNumber { get; set; }

        [Display(Name = "Immunization_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }

        public void Map(Model.PetVaccination immunization)
        {
            immunization.CustomVaccination = Immunization;
            immunization.InjectionDate = InjectionDate;
            immunization.HospitalName = new EncryptedText(HospitalName);
            immunization.SerialNumber = SerialNumber;
            immunization.Comment = new EncryptedText(Comment);
        }
    }
}