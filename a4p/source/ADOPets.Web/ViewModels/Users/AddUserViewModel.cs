using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System.Web.Mvc;
using ADOPets.Web.Common.Helpers;
using System.Globalization;
using System.Web.Security;
using Model.Tools;
namespace ADOPets.Web.ViewModels.Users
{
    public class AddUserViewModel
    {
        public AddUserViewModel()
        {
        }
        public int Id { get; set; }

        public int BasePlanId { get; set; }
        [Display(Name = "Users_AddUser_PlanName", ResourceType = typeof(Wording))]
        public string BasePlanName { get; set; }
        public decimal? BasePlanPrice { get; set; }

        #region Basic Info

        [Display(Name = "Users_AddUser_UserType", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_UserTypeIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public UserTypeEnum Usertype { get; set; }
        public int? UserTypeId { get; set; }

        [Display(Name = "Users_AddUser_Center", ResourceType = typeof(Wording))]
        public List<Model.Center> Center { get; set; }

        [Display(Name = "Users_AddUser_Center", ResourceType = typeof(Wording))]
        public int? CenterId { get; set; }

        [Display(Name = "Users_AddUser_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_FirstNameIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Users_AddUser_LastName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_LastNameIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Users_AddUser_DateOfBirth", ResourceType = typeof(Wording))]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessageResourceName = "Users_AddUser_EmailIsRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_Email", ResourceType = typeof(Wording))]
        [Remote("ValidateEmail", "Account", HttpMethod = "POST", ErrorMessageResourceName = "Account_SignUp_EmailIDExist", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Account_SignUp_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Users_AddUser_Gender", ResourceType = typeof(Wording))]
        public GenderEnum? Gender { get; set; }

        [Display(Name = "Users_AddUser_TimeZone", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_TimeZoneIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public TimeZoneEnum? TimeZone { get; set; }

        #endregion

        #region Address
        [Display(Name = "Users_AddUser_UploadPhoto", ResourceType = typeof(Wording))]
        public string Image { get; set; }

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
        public int? Zip { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_PrimaryPhone", ResourceType = typeof(Wording))]
        public string PrimaryPhone { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_SecondaryPhone", ResourceType = typeof(Wording))]
        public string SecondaryPhone { get; set; }

        #endregion

        #region OtherInfo


        [Display(Name = "Users_AddUser_Speciality", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsSpecialityRequired", TargetValue = true, ErrorMessageResourceName = "Users_AddUser_SpecialityIsRequired", ErrorMessageResourceType = typeof(Wording))]
        // [RequiredIfExpression(DependentProperties = new string[] { "Usertype", "Usertype", "Usertype" }, TargetValues = new Object[] { UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert }, ErrorMessageResourceName = "Users_AddUser_SpecialityIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public VetSpecialityEnum Speciality { get; set; }
        public int SpecialityId { get; set; }

        [Display(Name = "Users_AddUser_Hospital", ResourceType = typeof(Wording))]
        public string Hospital { get; set; }

        [Display(Name = "Users_AddUser_Diploma", ResourceType = typeof(Wording))]
        public string Diploma { get; set; }

        [Display(Name = "Users_AddUser_StateExercise", ResourceType = typeof(Wording))]
        public string StateExercise { get; set; }

        [Display(Name = "Users_AddUser_DEA", ResourceType = typeof(Wording))]
        public string DEA { get; set; }

        [Display(Name = "Users_AddUser_NPI", ResourceType = typeof(Wording))]
        public string NPI { get; set; }

        [Display(Name = "Users_AddUser_LicenceToPractice", ResourceType = typeof(Wording))]
        public string LicenceToPractice { get; set; }

        [Display(Name = "Users_AddUser_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        [Display(Name = "Users_AddUser_Plan", ResourceType = typeof(Wording))]
        public int PlanID { get; set; }

        [Display(Name = "Users_AddUser_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Users_AddUser_MaxPets", ResourceType = typeof(Wording))]
        public int? AdditionalPets { get; set; }

        [Display(Name = "Users_AddUser_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }


        public string Description { get; set; }

        public int? MaxPetCount { get; set; }



        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Dueration { get; set; }

        public int RemainingDays { get; set; }

        #endregion

        #region Login Info

        [Required(ErrorMessageResourceName = "Users_AddUser_UsernameRequired", ErrorMessageResourceType = typeof(Wording))]
        [Remote("ValidateUserName", "Account", HttpMethod = "POST", ErrorMessageResourceName = "Users_AddUser_UsernameExist", ErrorMessageResourceType = typeof(Wording))]
        [StringLength(30, ErrorMessageResourceName = "Users_AddUser_UsernameLessthan30", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_UserName", ResourceType = typeof(Wording))]
        public string Username { get; set; }

        //[Required(ErrorMessageResourceName = "Users_AddUser_PasswordIsRequired", ErrorMessageResourceType = typeof(Wording))]
        // [StringLength(100, ErrorMessageResourceName = "Account_SignUp_PasswordLength", ErrorMessageResourceType = typeof(Wording), MinimumLength = 8)]
        // [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z<>&'])(.{8,})$", ErrorMessageResourceName = "Account_SignUp_PasswordPolicy", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_Password", ResourceType = typeof(Wording))]
        public string Password { get; set; }
        #endregion


        public bool IsSpecialityRequired
        {
            get
            {
                bool result = Usertype.Equals(UserTypeEnum.VeterinarianAdo) || Usertype.Equals(UserTypeEnum.VeterinarianExpert) || Usertype.Equals(UserTypeEnum.VeterinarianLight);
                return result;
            }
        }

        public Model.User Map(byte[] data, Model.Subscription subscription)
        {
            var userObj = new Model.User
            {

                UserTypeId = Usertype,
                DomainTypeId = DomainHelper.GetDomain(),
                TimeZoneId = TimeZone,
                FirstName = new EncryptedText(FirstName),
                LastName = new EncryptedText(LastName),
                BirthDate = (BirthDate == null) ? new EncryptedText((Convert.ToDateTime(BirthDate)).ToString(CultureInfo.InvariantCulture)) : new EncryptedText(BirthDate.Value.ToString(CultureInfo.InvariantCulture)),
                GenderId = Gender,
                Email = new EncryptedText(Email.ToLower()),
                GeneralConditions = false,
                IsUsingDST = false,
                CreationDate = DateTime.Now,
                UserStatusId = UserStatusEnum.Active,
                PrimaryPhone = new EncryptedText(PrimaryPhone),
                SecondaryPhone = new EncryptedText(SecondaryPhone),
                InfoPath = string.Format("{0}\\{1}", DateTime.Today.Year, DateTime.Today.DayOfYear),
                ////ReferralSource = null,//new EncryptedText(ReferralSource),
                Address1 = new EncryptedText(Address1),
                Address2 = new EncryptedText(Address2),
                City = new EncryptedText(City),
                CountryId = Country,
                StateId = State,
                PostalCode = new EncryptedText(Convert.ToString(Zip)),
                IsNonSearchable = false,
                ProfileImage = data,
                CenterID = CenterId,
                IsEmailSent = false
            };

            var randomPart = Membership.GeneratePassword(5, 2);
            var credentials = new Model.Login
            {
                UserName = Encryption.Encrypt(Username),
                Password = Encryption.EncryptAsymetric(Username + randomPart),
                RandomPart = randomPart,
                IsTemporalPassword = true
            };
            userObj.Logins = new List<Model.Login> { credentials };

            var renewalDate = DateTime.Today;
            if (subscription.PaymentTypeId == PaymentTypeEnum.Yearly)
            {
                renewalDate = renewalDate.AddYears(1);
            }
            else if (subscription.PaymentTypeId == PaymentTypeEnum.Monthly)
            {
                renewalDate = renewalDate.AddMonths(1);
            }
            else if (subscription.PaymentTypeId == PaymentTypeEnum.SaleTransaction || subscription.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
            {
                //todo: refactor this
                renewalDate = renewalDate.AddYears(100);
            }
            else
            {
                renewalDate = renewalDate.AddDays(subscription.Duration);
            }

            var subscriptionService = new SubscriptionService
            {
                AditionalPetCount = AdditionalPets,
                AditionalMRACount = AdditionalPets,

            };

            bool isPaidUser = subscription.IsTrial == true ? true : false;
            if (DomainHelper.GetDomain() == DomainTypeEnum.US || DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                if (subscription.IsBase && subscription.PlanTypeId == PlanTypeEnum.BasicFree)//==> "Free Account With Limited Access")
                {
                    isPaidUser = true;
                }
            }
            userObj.UserSubscription = new UserSubscription
            {
                Subscription = subscription,
                StartDate = DateTime.Today,
                RenewalDate = renewalDate.AddDays(-1),
                SubscriptionExpirationAlertId = SubscriptionExpirationAlertEnum.NotSent,
                SubscriptionMailSent = true,
                SubscriptionService = subscriptionService,
                ispaymentDone = isPaidUser
            };

            if (Usertype.Equals(UserTypeEnum.VeterinarianAdo) || Usertype.Equals(UserTypeEnum.VeterinarianExpert) || Usertype.Equals(UserTypeEnum.VeterinarianLight))
            {
                var vetObj = new Model.Veterinarian
                  {
                      FirstName = new EncryptedText(FirstName),
                      LastName = new EncryptedText(LastName),
                      Email = new EncryptedText(Email),
                      IsEmergencyContact = false,
                      IsCurrentVeterinarian = false,
                      NPI = new EncryptedText(NPI),
                      HospitalName = new EncryptedText(Hospital),
                      Address1 = new EncryptedText(Address1),
                      Address2 = new EncryptedText(Address2),
                      City = new EncryptedText(City),
                      CountryId = Country,
                      StateId = State,
                      Zip = new EncryptedText(Convert.ToString(Zip)),
                      PhoneHome = new EncryptedText(PrimaryPhone),
                      PhoneOffice = new EncryptedText(SecondaryPhone),
                      Comment = new EncryptedText(Comments),
                      VetSpecialtyID = Speciality,
                      DEA = new EncryptedText(DEA),
                      Diploma = new EncryptedText(Diploma),
                      StateExercise = new EncryptedText(StateExercise),
                      LicenceToPractice = new EncryptedText(LicenceToPractice)
                  };


                userObj.Veterinarians = new List<Model.Veterinarian> { vetObj };
            }
            return userObj;
        }
    }
}