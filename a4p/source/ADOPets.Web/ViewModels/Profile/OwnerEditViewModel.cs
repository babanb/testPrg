using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ADOPets.Web.Common.Helpers;
using System.Web.Mvc;
namespace ADOPets.Web.ViewModels.Profile
{
    public class OwnerEditViewModel
    {
        public OwnerEditViewModel()
        {
        }
        public OwnerEditViewModel(User user)
        {
            OwnerId = user.Id;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            BirthDate = !string.IsNullOrEmpty(user.BirthDate)
                ? (DateTime?)Convert.ToDateTime(user.BirthDate, CultureInfo.InvariantCulture)
                : null;
            Gender = user.GenderId;
            Email = user.Email;
            Address1 = user.Address1;
            Address2 = user.Address2;
            City = user.City;
            Country = user.CountryId;
            State = user.StateId;
            Zip = user.PostalCode;
            PrimaryPhone = user.PrimaryPhone;
            SecondaryPhone = user.SecondaryPhone;
            TimeZone = user.TimeZoneId;
            Image = user.ProfileImage;
            IsSearchable = !user.IsNonSearchable;
            CreationDate = !string.IsNullOrEmpty(user.CreationDate.ToString())
                ? (DateTime?)Convert.ToDateTime(user.CreationDate, CultureInfo.InvariantCulture)
                : null;
        }

        public OwnerEditViewModel(User user, string vetspeciality)
        {
            OwnerId = user.Id;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            BirthDate = !string.IsNullOrEmpty(user.BirthDate)
                ? (DateTime?)Convert.ToDateTime(user.BirthDate, CultureInfo.InvariantCulture)
                : null;
            Gender = user.GenderId;
            Email = user.Email;
            Address1 = user.Address1;
            Address2 = user.Address2;
            City = user.City;
            Country = user.CountryId;
            State = user.StateId;
            Zip = user.PostalCode;
            PrimaryPhone = user.PrimaryPhone;
            SecondaryPhone = user.SecondaryPhone;
            TimeZone = user.TimeZoneId;
            Image = user.ProfileImage;
            IsSearchable = !user.IsNonSearchable;
            Speciality = vetspeciality;
            CreationDate = !string.IsNullOrEmpty(user.CreationDate.ToString())
           ? (DateTime?)Convert.ToDateTime(user.CreationDate, CultureInfo.InvariantCulture)
           : null;
        }

        [Display(Name = "Profile_OwnerEdit_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_OwnerEdit_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Profile_OwnerEdit_MiddleName", ResourceType = typeof(Wording))]
        public string MiddleName { get; set; }

        [Required(ErrorMessageResourceName = "Profile_OwnerEdit_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_OwnerEdit_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Profile_OwnerEdit_DateofBirth", ResourceType = typeof(Wording))]
        public DateTime? BirthDate { get; set; }

         [Display(Name = "Profile_OwnerEdit_CreationDate", ResourceType = typeof(Wording))]
        public DateTime? CreationDate { get; set; }        

        [Display(Name = "Profile_OwnerEdit_Gender", ResourceType = typeof(Wording))]
        public GenderEnum? Gender { get; set; }

        [Required(ErrorMessageResourceName = "Profile_OwnerEdit_EmailRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_OwnerEdit_Email", ResourceType = typeof(Wording))]
        [Remote("ValidateEmail", "Profile", HttpMethod = "POST", ErrorMessageResourceName = "Profile_OwnerEdit_EmailIDExist", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Profile_OwnerEdit_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Profile_OwnerEdit_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Profile_OwnerEdit_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Profile_OwnerEdit_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Profile_OwnerEdit_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Profile_OwnerEdit_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceName = "Profile_OwnerEdit_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_OwnerEdit_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Profile_OwnerEdit_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_OwnerEdit_PrimaryPhone", ResourceType = typeof(Wording))]
        public string PrimaryPhone { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Profile_OwnerEdit_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_OwnerEdit_SecondaryPhone", ResourceType = typeof(Wording))]
        public string SecondaryPhone { get; set; }

        [Required(ErrorMessageResourceName = "Profile_OwnerEdit_TimeZoneRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_OwnerEdit_TimeZone", ResourceType = typeof(Wording))]
        public TimeZoneEnum? TimeZone { get; set; }

        [Display(Name = "Profile_OwnerEdit_NonSearchable", ResourceType = typeof(Wording))]
        public bool IsSearchable { get; set; }

        public bool DeleteImg { get; set; }

        public byte[] Image { get; set; }

        public int OwnerId { get; set; }

        [Display(Name = "Profile_OwnerEdit_Speciality", ResourceType = typeof(Wording))]
        public string Speciality { get; set; }

        public void Map(User user, byte[] image)
        {
            user.DomainTypeId = DomainHelper.GetDomain();
            user.TimeZoneId = TimeZone;
            user.FirstName = new EncryptedText(FirstName);
            user.MiddleName = new EncryptedText(MiddleName);
            user.LastName = new EncryptedText(LastName);
            user.BirthDate = BirthDate.HasValue ? new EncryptedText(BirthDate.Value.ToString(CultureInfo.InvariantCulture)) : new EncryptedText(null);
            user.GenderId = Gender;
            user.Email = new EncryptedText(Email.ToLower());
            user.PrimaryPhone = new EncryptedText(PrimaryPhone);
            user.SecondaryPhone = new EncryptedText(SecondaryPhone);
            user.Address1 = new EncryptedText(Address1);
            user.Address2 = new EncryptedText(Address2);
            user.City = new EncryptedText(City);
            user.CountryId = Country;
            user.StateId = State;
            user.PostalCode = new EncryptedText(Zip);
            user.IsNonSearchable = !IsSearchable;
            if (image != null || DeleteImg)
            {
                user.ProfileImage = DeleteImg ? null : image;
            }
        }
    }
}