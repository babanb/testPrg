using ADOPets.Web.Resources;
using Model;
using Model.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ADOPets.Web.ViewModels.Users
{
    public class EditAdminViewModel
    {

        public EditAdminViewModel()
        {
        }

        public EditAdminViewModel(User user)
        {
            Id = user.Id;
            Usertype = user.UserTypeId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            BirthDate = !String.IsNullOrEmpty(user.BirthDate)
                ? Convert.ToDateTime(user.BirthDate, CultureInfo.InvariantCulture).ToShortDateString()
                : String.Empty;
            Gender = user.GenderId;
            TimeZone = user.TimeZoneId;
            Image = user.ProfileImage != null ? user.ProfileImage.ToString() : null;

            //Address
            Address1 = user.Address1;
            Address2 = user.Address2;
            City = user.City;
            State = user.StateId;
            Country = user.CountryId;
            Zip = user.PostalCode;
            PrimaryPhone = user.PrimaryPhone;
            SecondaryPhone = user.SecondaryPhone;

            //login Details
            var loginDetails = user.Logins.FirstOrDefault();
            if (loginDetails != null)
            {
                Username = Encryption.Decrypt(loginDetails.UserName);
                Password = "========";
            }

        }

        public int Id { get; set; }

        #region Basic Info

        [Display(Name = "Users_AddUser_UserType", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_UserTypeIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public UserTypeEnum Usertype { get; set; }
        public int? UserTypeId { get; set; }

        [Display(Name = "Users_AddUser_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_FirstNameIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Users_AddUser_LastName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_LastNameIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Users_AddUser_DateOfBirth", ResourceType = typeof(Wording))]
        public string BirthDate { get; set; }

        [Required(ErrorMessageResourceName = "Users_AddUser_EmailIsRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_Email", ResourceType = typeof(Wording))]
        [Remote("ValidateEmail", "Users", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "Profile_OwnerEdit_EmailIDExist", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Account_SignUp_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Users_AddUser_Gender", ResourceType = typeof(Wording))]
        public GenderEnum? Gender { get; set; }

        [Display(Name = "Users_AddUser_TimeZone", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_TimeZoneIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public TimeZoneEnum? TimeZone { get; set; }

        [Display(Name = "Users_AddUser_UploadPhoto", ResourceType = typeof(Wording))]
        public string Image { get; set; }

        public bool DeleteImg { get; set; }

        #endregion

        #region Address
        
        [Display(Name = "Users_AddUser_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Users_AddUser_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Users_AddUser_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Users_AddUser_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Users_AddUser_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Account_SignUp_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_PrimaryPhone", ResourceType = typeof(Wording))]
        public string PrimaryPhone { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_SecondaryPhone", ResourceType = typeof(Wording))]
        public string SecondaryPhone { get; set; }

        #endregion

        #region Login Info

        [Required(ErrorMessageResourceName = "Users_AddUser_UsernameRequired", ErrorMessageResourceType = typeof(Wording))]
        [Remote("ValidateUserName", "Users", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "Users_AddUser_UsernameExist", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_UserName", ResourceType = typeof(Wording))]
        [StringLength(30, ErrorMessageResourceName = "Users_AddUser_UsernameLessthan30", ErrorMessageResourceType = typeof(Wording))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "Users_AddUser_PasswordIsRequired", ErrorMessageResourceType = typeof(Wording))]
        //[StringLength(100, ErrorMessageResourceName = "Account_SignUp_PasswordLength", ErrorMessageResourceType = typeof(Wording), MinimumLength = 8)]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z<>&'])(.{8,})$", ErrorMessageResourceName = "Account_SignUp_PasswordPolicy", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_Password", ResourceType = typeof(Wording))]
        public string Password { get; set; }
        #endregion


        public void Map(Model.User user, byte[] data)
        {

            user.FirstName = new EncryptedText(FirstName);
            user.LastName = new EncryptedText(LastName);
            user.Email = new EncryptedText(Email); ;
            BirthDate = (BirthDate == null) ? new EncryptedText((Convert.ToDateTime(BirthDate)).ToString(CultureInfo.InvariantCulture)) : new EncryptedText(BirthDate.ToString(CultureInfo.InvariantCulture));
            user.GenderId = Gender;
            user.TimeZoneId = TimeZone;
            user.Email = new EncryptedText(Email.ToLower());
            user.PrimaryPhone = new EncryptedText(PrimaryPhone);
            user.SecondaryPhone = new EncryptedText(SecondaryPhone);
            user.Address1 = new EncryptedText(Address1);
            user.Address2 = new EncryptedText(Address2);
            user.City = new EncryptedText(City);
            user.CountryId = Country;
            user.StateId = State;
            user.PostalCode = new EncryptedText(Convert.ToString(Zip));
            if (data != null || DeleteImg)
            {
                user.ProfileImage = data;
            }
        }

    }
}