using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.PetContact
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petid)
        {
            PetId = petid;

            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                Country = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                Country = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                Country = CountryEnum.France;
            }
        }

        public int PetId { get; set; }

        [Display(Name = "PetContact_Add_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PetContact_Add_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "PetContact_Add_LastName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PetContact_Add_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "PetContact_Add_Relationship", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PetContact_Add_RelationshipRequired", ErrorMessageResourceType = typeof(Wording))]
        public ContactTypeEnum Relationship { get; set; }

        [Display(Name = "PetContact_Add_IsEmergencyContact", ResourceType = typeof(Wording))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "PetContact_Add_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PetContact_Add_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "PetContact_Add_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "PetContact_Add_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "PetContact_Add_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "PetContact_Add_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "PetContact_Add_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "PetContact_Add_Zip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "PetContact_Add_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "PetContact_Add_Home", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Add_HomeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Home { get; set; }

        [Display(Name = "PetContact_Add_Office", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Add_OfficeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Office { get; set; }

        [Display(Name = "PetContact_Add_Cell", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Add_CellNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Cell { get; set; }

        [Display(Name = "PetContact_Add_Fax", ResourceType = typeof(Wording))]
        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "PetContact_Add_FaxFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Fax { get; set; }

        [Display(Name = "PetContact_Add_Email", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "PetContact_Add_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "PetContact_Add_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }



        public Model.PetContact Map(PetTypeEnum petType, int petId, int userId)
        {
            var contact = new Model.Contact
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
                //PetTypeId = petType,
                PetId = PetId
            };

            var petcontact = new Model.PetContact
            {
                ContactTypeId = Relationship,
                FirstName = new EncryptedText(FirstName),
                LastName = new EncryptedText(LastName),
                IsEmergencyContact = IsEmergencyContact,
                StartDate = StartDate,
                EndDate = EndDate,
                Address1 = new EncryptedText(Address1),
                Address2 = new EncryptedText(Address2),
                CountryId = Country,
                StateId = State,
                City = new EncryptedText(City),
                Zip = new EncryptedText(Zip),
                PhoneHome = new EncryptedText(Home),
                PhoneOffice = new EncryptedText(Office),
                PhoneCell = new EncryptedText(Cell),
                Fax = new EncryptedText(Fax),
                Email = new EncryptedText(Email),
                Comments = new EncryptedText(Comments),
                PetId = PetId,
                Contacts = IsEmergencyContact ? new List<Model.Contact> { contact } : null

            };

            return petcontact;
        }


        public Model.PetContact Map()
        {
            var petcontact = new Model.PetContact
            {
                ContactTypeId = Relationship,
                FirstName = new EncryptedText(FirstName),
                LastName = new EncryptedText(LastName),
                IsEmergencyContact = IsEmergencyContact,
                StartDate = StartDate,
                EndDate = EndDate,
                Address1 = new EncryptedText(Address1),
                Address2 = new EncryptedText(Address2),
                CountryId = Country,
                StateId = State,
                City = new EncryptedText(City),
                Zip = new EncryptedText(Zip),
                PhoneHome = new EncryptedText(Home),
                PhoneOffice = new EncryptedText(Office),
                PhoneCell = new EncryptedText(Cell),
                Fax = new EncryptedText(Fax),
                Email = new EncryptedText(Email),
                Comments = new EncryptedText(Comments),
                PetId = PetId,
            };

            return petcontact;
        }
    }
}