using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Message
{
    public class InviteViewModel
    {
        [Display(Name = "Message_Invite_FirstName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Message_Invite_FirstNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FirstName { get; set; }


        [Required(ErrorMessageResourceName = "Message_Invite_LastNameRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Message_Invite_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }


        [Required(ErrorMessageResourceName = "Message_Invite_EmailRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Message_Invite_Email", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Message_Invite_InvalidEmail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Message_Invite_Message", ResourceType = typeof(Wording))]
        public string Message { get; set; }

        [Display(Name = "Message_Invite_Veterinarian", ResourceType = typeof(Wording))]
        public bool IsVet { get; set; }

        [Display(Name = "Message_Invite_Friend", ResourceType = typeof(Wording))]
        public bool IsFriend { get; set; }
    }
}