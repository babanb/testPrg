using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using ADOPets.Web.Resources;
using Model.Tools;

namespace ADOPets.Web.ViewModels.Profile
{
    public class ChangePasswordViewModel
    {
        public ChangePasswordViewModel()
        {
        }

        [Required(ErrorMessageResourceName = "Profile_ChangePassword_OldPasswordRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_ChangePassword_OldPassword", ResourceType = typeof(Wording))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Profile_ChangePassword_PasswordRequired", ErrorMessageResourceType = typeof(Wording))]
        [StringLength(100, ErrorMessageResourceName = "Profile_ChangePassword_PasswordLength", ErrorMessageResourceType = typeof(Wording), MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,})$", ErrorMessageResourceName = "Profile_ChangePassword_PasswordPolicy", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_ChangePassword_NewPassword", ResourceType = typeof(Wording))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "Profile_ChangePassword_ConfirmPasswordRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_ChangePassword_ConfirmPassword", ResourceType = typeof(Wording))]
        [Compare("NewPassword", ErrorMessageResourceName = "Profile_ChangePassword_PasswordNotMatch", ErrorMessageResourceType = typeof(Wording))]
        public string ConfirmPassword { get; set; }

        public void Map(Model.Login login)
        {
            var randomPart = Membership.GeneratePassword(5, 2);
            login.RandomPart = randomPart;
            login.Password = Encryption.EncryptAsymetric(ConfirmPassword + randomPart);
        }
    }
}