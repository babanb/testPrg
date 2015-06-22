using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using ADOPets.Web.Resources;
using Model.Tools;

namespace ADOPets.Web.ViewModels.Profile
{
    public class ForceToChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Profile_ForceToChangePassword_OldPasswordRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_ForceToChangePassword_OldPassword", ResourceType = typeof(Wording))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Profile_ForceToChangePassword_PasswordRequired", ErrorMessageResourceType = typeof(Wording))]
        [StringLength(100, ErrorMessageResourceName = "Profile_ForceToChangePassword_PasswordLength", ErrorMessageResourceType = typeof(Wording), MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,})$", ErrorMessageResourceName = "Profile_ForceToChangePassword_PasswordPolicy", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_ForceToChangePassword_NewPassword", ResourceType = typeof(Wording))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "Profile_ForceToChangePassword_ConfirmPasswordRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_ForceToChangePassword_ConfirmPassword", ResourceType = typeof(Wording))]
        [Compare("NewPassword", ErrorMessageResourceName = "Profile_ForceToChangePassword_PasswordNotMatch", ErrorMessageResourceType = typeof(Wording))]
        public string ConfirmPassword { get; set; }

        public void Map(Model.Login login)
        {
            var randomPart = Membership.GeneratePassword(5, 2);
            login.RandomPart = randomPart;
            login.Password = Encryption.EncryptAsymetric(ConfirmPassword + randomPart);
        }
    }
}