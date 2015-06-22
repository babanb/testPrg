using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;
using Model.Tools;

namespace ADOPets.Web.ViewModels.Profile
{
    public class AddViewModel
    {
        [Display(Name = "Profile_Add_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_Add_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Profile_Add_LastName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_Add_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Profile_Add_Email", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_Add_EmailRequired", ErrorMessageResourceType = typeof(Wording))]
        [Remote("ValidateEmail", "Account", HttpMethod = "POST", ErrorMessageResourceName = "Profile_Add_EmailAlreadyExist", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Profile_Add_EmailInvalid", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Profile_Add_UserName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_Add_UserNameRequired", ErrorMessageResourceType = typeof(Wording))]
        [Remote("ValidateUserName", "Account", HttpMethod = "POST", ErrorMessageResourceName = "Profile_Add_UserNameInvalid", ErrorMessageResourceType = typeof(Wording))]
        public string UserName { get; set; }

        [Display(Name = "Profile_Add_Payment", ResourceType = typeof(Wording))]
        public bool MustPay { get; set; }

        [Display(Name = "Profile_Add_PasswordTemporal", ResourceType = typeof(Wording))]
        public bool MustChangePassword { get; set; }

        [Display(Name = "Profile_Add_TermsAndConditions", ResourceType = typeof(Wording))]
        public bool MustAcceptTermsAndConditions { get; set; }

        [Display(Name = "Profile_Add_Subscription", ResourceType = typeof(Wording))]
        public int Subscription { get; set; }

        public User Map(Model.Subscription subscription, string password)
        {
            var user = new User
            {
                UserTypeId = UserTypeEnum.OwnerAdmin,
                DomainTypeId = DomainHelper.GetDomain(),
                FirstName = new EncryptedText(FirstName),
                LastName = new EncryptedText(LastName),
                Email = new EncryptedText(Email),
                GeneralConditions = !MustAcceptTermsAndConditions,
                //todo MustPay
                CreationDate = DateTime.Now,
                UserStatusId = UserStatusEnum.Active,
                InfoPath = string.Format("{0}\\{1}", DateTime.Today.Year, DateTime.Today.DayOfYear),
            };

            var randomPart = Membership.GeneratePassword(5, 2);
            var credentials = new Login
            {
                UserName = Encryption.Encrypt(UserName),
                Password = Encryption.EncryptAsymetric(password + randomPart),
                RandomPart = randomPart,
                IsTemporalPassword = MustChangePassword
            };
            user.Logins = new List<Login> { credentials };

            user.UserSubscription = new UserSubscription
            {
                Subscription = subscription,
                SubscriptionExpirationAlertId = SubscriptionExpirationAlertEnum.NotSent,
                SubscriptionMailSent = true
            };

            return user;

        }
    }
}