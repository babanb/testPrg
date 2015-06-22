using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;


namespace ADOPets.Web.Controllers
{
     [AllowAnonymous]

   public class PrivacyAndPolicyController : BaseController
    {

        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("PrivacyAndPolicy");
        }

    }
}
