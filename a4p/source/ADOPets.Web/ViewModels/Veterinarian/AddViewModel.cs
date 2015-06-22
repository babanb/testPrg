using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;
using System.Collections.Generic;
using ADOPets.Web.Common.Helpers;


namespace ADOPets.Web.ViewModels.Veterinarian
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


        [Display(Name = "Veterinarian_Add_LastName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Veterinarian_Add_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string LastName { get; set; }


        [Display(Name = "Veterinarian_Add_MiddleName", ResourceType = typeof(Wording))]
        public string MiddleName { get; set; }

        [Display(Name = "Veterinarian_Add_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Veterinarian_Add_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Veterinarian_Add_IsEmergencyContact", ResourceType = typeof(Wording))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "Veterinarian_Add_IsCurrentVeterinarian", ResourceType = typeof(Wording))]
        public bool IsCurrentVeterinarian { get; set; }


        [Display(Name = "Veterinarian_Add_StartDate", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsCurrentVeterinarian", TargetValue = true, ErrorMessageResourceName = "Veterinarian_Add_StartDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Veterinarian_Add_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Veterinarian_Add_NPI", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsCurrentVeterinarian", TargetValue = true, ErrorMessageResourceName = "Veterinarian_Add_NPIRequired", ErrorMessageResourceType = typeof(Wording))]
        public string NPI { get; set; }

        [Display(Name = "Veterinarian_Add_HospitalName", ResourceType = typeof(Wording))]
        public string HospitalName { get; set; }

        [Display(Name = "Veterinarian_Add_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Veterinarian_Add_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Veterinarian_Add_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Veterinarian_Add_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "Veterinarian_Add_VetSpeciality", ResourceType = typeof(Wording))]
        public VetSpecialityEnum? VetSpeciality { get; set; }

        [Display(Name = "Veterinarian_Add_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Veterinarian_Add_Zip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Veterinarian_Add_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "Veterinarian_Add_Home", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Add_HomeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Home { get; set; }

        [Display(Name = "Veterinarian_Add_Office", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Add_OfficeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Office { get; set; }

        [Display(Name = "Veterinarian_Add_Cell", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Add_CellNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Cell { get; set; }

        [Display(Name = "Veterinarian_Add_Fax", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Veterinarian_Add_FaxFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Fax { get; set; }

        [Display(Name = "Veterinarian_Add_Email", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Veterinarian_Add_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Veterinarian_Add_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public int UserId { get; set; }

        public Model.Veterinarian Map(PetTypeEnum petType, int petId, int userId)
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
                ContactTypeId = ContactTypeEnum.Veterinarian,
                UserId = userId,
                // PetTypeId = petType,
                PetId = petId
            };

            var veterinarian = new Model.Veterinarian
            {
                IsCurrentVeterinarian = IsCurrentVeterinarian,
                IsEmergencyContact = IsEmergencyContact,
                StartDate = StartDate,
                EndDate = EndDate,
                FirstName = new EncryptedText(FirstName),
                LastName = new EncryptedText(LastName),
                MiddleName = new EncryptedText(MiddleName),
                NPI = new EncryptedText(NPI),
                HospitalName = new EncryptedText(HospitalName),
                Address1 = new EncryptedText(Address1),
                Address2 = new EncryptedText(Address2),
                CountryId = Country,
                StateId = State,
                VetSpecialtyID = VetSpeciality,
                City = new EncryptedText(City),
                Zip = new EncryptedText(Zip),
                PhoneHome = new EncryptedText(Home),
                PhoneOffice = new EncryptedText(Office),
                PhoneCell = new EncryptedText(Cell),
                Fax = new EncryptedText(Fax),
                Email = new EncryptedText(Email),
                Comment = new EncryptedText(Comments),
                PetId = PetId,
                Contacts = IsEmergencyContact ? new List<Model.Contact> { contact } : null
            };

            return veterinarian;
        }

        public Model.Veterinarian Map()
        {
            var veterinarian = new Model.Veterinarian
            {
                IsCurrentVeterinarian = IsCurrentVeterinarian,
                IsEmergencyContact = IsEmergencyContact,
                StartDate = StartDate,
                EndDate = EndDate,
                FirstName = new EncryptedText(FirstName),
                LastName = new EncryptedText(LastName),
                MiddleName = new EncryptedText(MiddleName),
                NPI = new EncryptedText(NPI),
                HospitalName = new EncryptedText(HospitalName),
                Address1 = new EncryptedText(Address1),
                Address2 = new EncryptedText(Address2),
                CountryId = Country,
                StateId = State,
                VetSpecialtyID = VetSpeciality,
                City = new EncryptedText(City),
                Zip = new EncryptedText(Zip),
                PhoneHome = new EncryptedText(Home),
                PhoneOffice = new EncryptedText(Office),
                PhoneCell = new EncryptedText(Cell),
                Fax = new EncryptedText(Fax),
                Email = new EncryptedText(Email),
                Comment = new EncryptedText(Comments),
                PetId = PetId,
            };

            return veterinarian;
        }
    }
}