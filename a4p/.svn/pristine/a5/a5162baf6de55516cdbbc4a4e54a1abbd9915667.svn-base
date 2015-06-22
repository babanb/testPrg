using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Account
{
    public class BasicInfoViewModel
    {
        public string PlanType { get; set; }


        [Display(Name = "Account_SignUp_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        public int PetCount { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal? Price { get; set; }

        public int BasePlanId { get; set; }
        public string BasePlanName { get; set; }
        public string BasePlanDescription { get; set; }
        public decimal? BasePlanPrice { get; set; }

        public int AdditionalPetCount { get; set; }

        [Display(Name = "Account_SignUp_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Account_SignUp_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Account_SignUp_MiddleName", ResourceType = typeof(Wording))]        
        public string MiddleName { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        //[Required(ErrorMessageResourceName = "Account_SignUp_DateofBirthRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_DateofBirth", ResourceType = typeof(Wording))]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Account_SignUp_Gender", ResourceType = typeof(Wording))]
        public GenderEnum? Gender { get; set; }
        
        [Required(ErrorMessageResourceName = "Account_SignUp_EmailRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_Email", ResourceType = typeof(Wording))]
        [Remote("ValidateEmail", "Account", HttpMethod = "POST", ErrorMessageResourceName = "Account_SignUp_EmailIDExist", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("^\\w+([-+.]*[\\w-]+)*@(\\w+([-.]?\\w+)){1,}\\.\\w{2,4}$", ErrorMessageResourceName = "Account_SignUp_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Account_SignUp_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Account_SignUp_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Account_SignUp_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Account_SignUp_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Account_SignUp_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Account_SignUp_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_Zip", ResourceType = typeof(Wording))]
        public int? Zip { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_PrimaryPhone", ResourceType = typeof(Wording))]
        public string PrimaryPhone { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]        
        [Display(Name = "Account_SignUp_SecondaryPhone", ResourceType = typeof(Wording))]
        public string SecondaryPhone { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_TimeZoneRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_TimeZone", ResourceType = typeof(Wording))]
        public TimeZoneEnum? TimeZone { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_ReferenceRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_Reference", ResourceType = typeof(Wording))]
        public string Reference { get; set; }

        [Display(Name = "Account_SignUp_HealthProvider", ResourceType = typeof(Wording))]
        public bool IsHealthProvider { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_UsernameRequired", ErrorMessageResourceType = typeof(Wording))]
        [Remote("ValidateUserName", "Account", HttpMethod = "POST", ErrorMessageResourceName = "Account_SignUp_UsernameExist", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_Username", ResourceType = typeof(Wording))]
        [StringLength(30, ErrorMessageResourceName = "Users_AddUser_UsernameLessthan30", ErrorMessageResourceType = typeof(Wording))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_PasswordRequired", ErrorMessageResourceType = typeof(Wording))]        
        [StringLength(100, ErrorMessageResourceName = "Account_SignUp_PasswordLength", ErrorMessageResourceType = typeof(Wording), MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z<>&'])(.{8,})$", ErrorMessageResourceName = "Account_SignUp_PasswordPolicy", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_Password", ResourceType = typeof(Wording))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_ConfirmPasswordRequired", ErrorMessageResourceType = typeof(Wording))]       
        [Display(Name = "Account_SignUp_ConfirmPassword", ResourceType = typeof(Wording))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "Account_SignUp_PasswordNotMatch", ErrorMessageResourceType = typeof(Wording))]
        public string ConfirmPassword { get; set; }

        public int UserRoleId { get; set; }

         [Display(Name = "Account_SignUp_Howdidyouhearaboutus", ResourceType = typeof(Wording))]
        public ReferralSourceEnum? ReferralSource { get; set; }
        
   }

}