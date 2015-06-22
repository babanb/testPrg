using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ExpressiveAnnotations.Attributes;
using System.Collections.Generic;

namespace ADOPets.Web.ViewModels.Veterinarian
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.Veterinarian veterinarian)
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
            //VetSpecialities=VetSpecialities.SpecialityId,
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
        [Required(ErrorMessageResourceName = "Veterinarian_Edit_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string LastName { get; set; }


        [Display(Name = "Veterinarian_Edit_MiddleName", ResourceType = typeof(Wording))]
        public string MiddleName { get; set; }

        [Display(Name = "Veterinarian_Edit_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Veterinarian_Edit_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Veterinarian_Edit_IsEmergencyContact", ResourceType = typeof(Wording))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "Veterinarian_Edit_IsCurrentVeterinarian", ResourceType = typeof(Wording))]
        public bool IsCurrentVeterinarian { get; set; }


        [Display(Name = "Veterinarian_Edit_StartDate", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsCurrentVeterinarian", TargetValue = true, ErrorMessageResourceName = "Veterinarian_Edit_StartDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Veterinarian_Edit_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Veterinarian_Edit_NPI", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsCurrentVeterinarian", TargetValue = true, ErrorMessageResourceName = "Veterinarian_Edit_NPIRequired", ErrorMessageResourceType = typeof(Wording))]
        public string NPI { get; set; }

        [Display(Name = "Veterinarian_Edit_HospitalName", ResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Veterinarian_Edit_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Veterinarian_Edit_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Veterinarian_Edit_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Veterinarian_Edit_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "Veterinarian_Edit_VetSpeciality", ResourceType = typeof(Wording))]
        public VetSpecialityEnum? VetSpeciality { get; set; }


        [Display(Name = "Veterinarian_Edit_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Veterinarian_Edit_Zip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Veterinarian_Edit_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "Veterinarian_Edit_Home", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Edit_HomeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Home { get; set; }

        [Display(Name = "Veterinarian_Edit_Office", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Edit_OfficeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Office { get; set; }

        [Display(Name = "Veterinarian_Edit_Cell", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Edit_CellNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Cell { get; set; }

        [Display(Name = "Veterinarian_Edit_Fax", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Edit_FaxFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Fax { get; set; }

        [Display(Name = "Veterinarian_Edit_Email", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Veterinarian_Edit_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Veterinarian_Edit_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public int? PetId { get; set; }

        public void Map(Model.Veterinarian veterinarian, Model.Contact updateContact, int userId, PetTypeEnum petType, string petName)
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
                        ContactTypeId = ContactTypeEnum.Veterinarian,
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
            veterinarian.IsCurrentVeterinarian = IsCurrentVeterinarian;
            veterinarian.IsEmergencyContact = IsEmergencyContact;
            veterinarian.StartDate = StartDate;
            veterinarian.EndDate = EndDate;
            veterinarian.FirstName = new EncryptedText(FirstName);
            veterinarian.MiddleName = new EncryptedText(MiddleName);
            veterinarian.LastName = new EncryptedText(LastName);
            veterinarian.NPI = new EncryptedText(NPI);
            veterinarian.HospitalName = new EncryptedText(HospitalName);
            veterinarian.Address1 = new EncryptedText(Address1);
            veterinarian.Address2 = new EncryptedText(Address2);
            veterinarian.CountryId = Country;
            veterinarian.StateId = State;
            veterinarian.VetSpecialtyID = VetSpeciality;
            veterinarian.City = new EncryptedText(City);
            veterinarian.Zip = new EncryptedText(Zip);
            veterinarian.PhoneHome = new EncryptedText(Home);
            veterinarian.PhoneOffice = new EncryptedText(Office);
            veterinarian.PhoneCell = new EncryptedText(Cell);
            veterinarian.Fax = new EncryptedText(Fax);
            veterinarian.Email = new EncryptedText(Email);
            veterinarian.Comment = new EncryptedText(Comments);
            if (IsEmergencyContact) { veterinarian.Contacts = updateContact == null ? new List<Model.Contact> { contact } : new List<Model.Contact> { updateContact }; }
        }
    }
}