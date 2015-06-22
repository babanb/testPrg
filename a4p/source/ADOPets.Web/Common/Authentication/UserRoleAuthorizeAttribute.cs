using System.Linq;

namespace ADOPets.Web.Common.Authentication
{
    public class UserRoleAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public UserRoleAuthorizeAttribute() { }

        public UserRoleAuthorizeAttribute(params Model.UserTypeEnum[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }
    }
}