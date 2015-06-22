using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Account_SignUp_EmailRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_Email", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Account_SignUp_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }
    }
}