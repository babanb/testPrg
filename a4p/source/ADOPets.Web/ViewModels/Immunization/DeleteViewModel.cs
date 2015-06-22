using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Immunization
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetVaccination immunization)
        {
            Id= immunization.Id;
            Immunization = immunization.CustomVaccination;
            InjectionDate = immunization.InjectionDate;
            HospitalName = immunization.HospitalName;
            SerialNumber = immunization.SerialNumber;
            Comment = immunization.Comment;
            PetId = immunization.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Immunization_Delete_Immunization", ResourceType = typeof(Wording))]
        public string Immunization { get; set; }

        [Display(Name = "Immunization_Delete_InjectionDate", ResourceType = typeof(Wording))]
        public DateTime InjectionDate { get; set; }

        [Display(Name = "Immunization_Delete_HospitalName", ResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Immunization_Delete_SerialNumber", ResourceType = typeof(Wording))]
        public string SerialNumber { get; set; }

        [Display(Name = "Immunization_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }
    }
}