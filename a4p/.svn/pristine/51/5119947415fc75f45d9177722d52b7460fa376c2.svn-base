using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.PetContact
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetContact petcontact)
        {
            Id = petcontact.Id;
            IsEmergencyContact = petcontact.IsEmergencyContact;
            StartDate = petcontact.StartDate;
            EndDate = petcontact.EndDate;
            FirstName = petcontact.FirstName;
            LastName = petcontact.LastName;
            Relationship = petcontact.ContactTypeId;
            Address1 = petcontact.Address1;
            Address2 = petcontact.Address2;
            Country = petcontact.CountryId;
            State = petcontact.StateId;
            City = petcontact.City;
            Zip = petcontact.Zip;
            Home = petcontact.PhoneHome;
            Office = petcontact.PhoneOffice;
            Cell = petcontact.PhoneCell;
            Fax = petcontact.Fax;
            Email = petcontact.Email;
            Comments = petcontact.Comments;
        }

        public int Id { get; set; }

        [Display(Name = "PetContact_Delete_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "PetContact_Delete_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "PetContact_Delete_Relationship", ResourceType = typeof(Wording))]
        public ContactTypeEnum Relationship { get; set; }

        [Display(Name = "PetContact_Delete_IsEmergencyContact", ResourceType = typeof(Wording))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "PetContact_Delete_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PetContact_Delete_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "PetContact_Delete_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "PetContact_Delete_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "PetContact_Delete_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "PetContact_Delete_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "PetContact_Delete_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "PetContact_Delete_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "PetContact_Delete_Home", ResourceType = typeof(Wording))]
        public string Home { get; set; }

        [Display(Name = "PetContact_Delete_Office", ResourceType = typeof(Wording))]
        public string Office { get; set; }

        [Display(Name = "PetContact_Delete_Cell", ResourceType = typeof(Wording))]
        public string Cell { get; set; }

        [Display(Name = "PetContact_Delete_Fax", ResourceType = typeof(Wording))]
        public string Fax { get; set; }

        [Display(Name = "PetContact_Delete_Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "PetContact_Delete_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public int PetId { get; set; }

    }
}