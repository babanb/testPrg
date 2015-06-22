using System.ComponentModel.DataAnnotations;

namespace ADOPets.Web.ViewModels.Account
{
    public class Login
    {
        [Display(Name = "Account_Login_UserName", ResourceType = typeof(Resources.Wording))]
        [Required(ErrorMessageResourceName = "Account_Login_UsernameRequired", ErrorMessageResourceType = typeof(Resources.Wording))]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Account_Login_Password", ResourceType = typeof(Resources.Wording))]
        [Required(ErrorMessageResourceName = "Account_Login_PasswordRequired", ErrorMessageResourceType = typeof(Resources.Wording))]
        public string Password { get; set; }

        [Display(Name = "Account_Login_RememberMe", ResourceType = typeof(Resources.Wording))]
        public bool RememberMe { get; set; }
    }
}