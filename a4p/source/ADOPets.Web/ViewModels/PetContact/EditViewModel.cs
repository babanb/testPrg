using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using System.Collections.Generic;

namespace ADOPets.Web.ViewModels.PetContact
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetContact petcontact)
        {
            Id = petcontact.Id;
            FirstName = petcontact.FirstName;
            LastName = petcontact.LastName;
            IsEmergencyContact = petcontact.IsEmergencyContact;
            StartDate = petcontact.StartDate;
            EndDate = petcontact.EndDate;
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
            PetId = petcontact.PetId;
            Relationship = petcontact.ContactTypeId;
        }

        public int Id { get; set; }

        [Display(Name = "PetContact_Edit_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PetContact_Edit_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "PetContact_Edit_LastName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PetContact_Edit_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "PetContact_Edit_Relationship", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PetContact_Edit_RelationshipRequired", ErrorMessageResourceType = typeof(Wording))]
        public ContactTypeEnum Relationship { get; set; }

        [Display(Name = "PetContact_Edit_IsEmergencyContact", ResourceType = typeof(Wording))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "PetContact_Edit_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PetContact_Edit_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "PetContact_Edit_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "PetContact_Edit_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "PetContact_Edit_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "PetContact_Edit_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "PetContact_Edit_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "PetContact_Edit_Zip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "PetContact_Edit_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "PetContact_Edit_Home", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Edit_HomeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Home { get; set; }

        [Display(Name = "PetContact_Edit_Office", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Edit_OfficeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Office { get; set; }

        [Display(Name = "PetContact_Edit_Cell", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Edit_CellNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Cell { get; set; }

        [Display(Name = "PetContact_Edit_Fax", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Edit_FaxFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Fax { get; set; }

        [Display(Name = "PetContact_Edit_Email", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "PetContact_Edit_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "PetContact_Edit_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public int PetId { get; set; }



        public void Map(Model.PetContact petcontact, Model.Contact updateContact, int userId, PetTypeEnum petType, string petName)
        {
            Model.Contact contact = null;
            if (IsEmergencyContact)
            {
                if (updateContact == null)
                {
                    contact = new Model.Contact
                    {
                        FirstName = new EncryptedText(FirstName),
                        LastName = new EncryptedText(LastName),
                        PhoneHome = new EncryptedText(Home),
                        PhoneOffice = new EncryptedText(Office),
                        PhoneCell = new EncryptedText(Cell),
                        Fax = new EncryptedText(Fax),
                        Email = new EncryptedText(Email),
                        ContactTypeId = Relationship,
                        UserId = userId,
                        PetId = PetId
                    };
                }
                else
                {
                    updateContact.FirstName = new EncryptedText(FirstName);
                    updateContact.LastName = new EncryptedText(LastName);
                    updateContact.PhoneHome = new EncryptedText(Home);
                    updateContact.PhoneOffice = new EncryptedText(Office);
                    updateContact.PhoneCell = new EncryptedText(Cell);
                    updateContact.Fax = new EncryptedText(Fax);
                    updateContact.Email = new EncryptedText(Email);

                }
            }
            petcontact.ContactTypeId = Relationship;
            petcontact.FirstName = new EncryptedText(FirstName);
            petcontact.LastName = new EncryptedText(LastName);
            petcontact.IsEmergencyContact = IsEmergencyContact;
            petcontact.StartDate = StartDate;
            petcontact.EndDate = EndDate;
            petcontact.Address1 = new EncryptedText(Address1);
            petcontact.Address2 = new EncryptedText(Address2);
            petcontact.CountryId = Country;
            petcontact.StateId = State;
            petcontact.City = new EncryptedText(City);
            petcontact.Zip = new EncryptedText(Zip);
            petcontact.PhoneHome = new EncryptedText(Home);
            petcontact.PhoneOffice = new EncryptedText(Office);
            petcontact.PhoneCell = new EncryptedText(Cell);
            petcontact.Fax = new EncryptedText(Fax);
            petcontact.Email = new EncryptedText(Email);
            petcontact.Comments = new EncryptedText(Comments);

            if (IsEmergencyContact)
            {
                petcontact.Contacts = updateContact == null ? new List<Model.Contact> { contact } : new List<Model.Contact> { updateContact };
            }
        }
    }
}