using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Veterinarian
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.Veterinarian veterinarian)
        {
            Id = veterinarian.Id;
            IsCurrentVeterinarian = veterinarian.IsCurrentVeterinarian;
            IsEmergencyContact = veterinarian.IsEmergencyContact;
            StartDate = veterinarian.StartDate;
            EndDate = veterinarian.EndDate;
            FirstName = veterinarian.FirstName;
            MiddleName = veterinarian.MiddleName;
            LastName = veterinarian.LastName;
            NPI = veterinarian.NPI;
            HospitalName = veterinarian.HospitalName;
            Address1 = veterinarian.Address1;
            Address2 = veterinarian.Address2;
            Country = veterinarian.CountryId;
            State = veterinarian.StateId;
            VetSpeciality = veterinarian.VetSpecialtyID;
            City = veterinarian.City;
            Zip = veterinarian.Zip;
            Home = veterinarian.PhoneHome;
            Office = veterinarian.PhoneOffice;
            Cell = veterinarian.PhoneCell;
            Fax = veterinarian.Fax;
            Email = veterinarian.Email;
            Comments = veterinarian.Comment;
            PetId = veterinarian.PetId;

        }

        public int Id { get; set; }

        [Display(Name = "Veterinarian_Edit_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Veterinarian_Edit_MiddleName", ResourceType = typeof(Wording))]
        public string MiddleName { get; set; }

        [Display(Name = "Veterinarian_Edit_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Veterinarian_Edit_IsEmergencyContact", ResourceType = typeof(Wording))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "Veterinarian_Edit_IsCurrentVeterinarian", ResourceType = typeof(Wording))]
        public bool IsCurrentVeterinarian { get; set; }


        [Display(Name = "Veterinarian_Edit_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Veterinarian_Edit_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Veterinarian_Edit_NPI", ResourceType = typeof(Wording))]
        public string NPI { get; set; }

        [Display(Name = "Veterinarian_Edit_HospitalName", ResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Veterinarian_Edit_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Veterinarian_Edit_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Veterinarian_Edit_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Veterinarian_Edit_VetSpeciality", ResourceType = typeof(Wording))]
        public VetSpecialityEnum? VetSpeciality { get; set; }

        [Display(Name = "Veterinarian_Edit_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "Veterinarian_Edit_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Veterinarian_Edit_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "Veterinarian_Edit_Home", ResourceType = typeof(Wording))]
        public string Home { get; set; }

        [Display(Name = "Veterinarian_Edit_Office", ResourceType = typeof(Wording))]
        public string Office { get; set; }

        [Display(Name = "Veterinarian_Edit_Cell", ResourceType = typeof(Wording))]
        public string Cell { get; set; }

        [Display(Name = "Veterinarian_Edit_Fax", ResourceType = typeof(Wording))]
        public string Fax { get; set; }

        [Display(Name = "Veterinarian_Edit_Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Veterinarian_Edit_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public int? PetId { get; set; }


    }
}