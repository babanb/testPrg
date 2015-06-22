using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;

namespace ADOPets.Web.Controllers
{
    [AllowAnonymous]
    public class TermAndConditionController : BaseController
    {
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("TermAndCondition");
        }

        public ActionResult Accept(bool accept, string returnUrl)
        {
            if (accept)
            {
                var userId = HttpContext.User.GetUserId();

                var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId);
                user.GeneralConditions = true;
                UnitOfWork.UserRepository.Update(user);
                UnitOfWork.Save();

                HttpContext.User.UpdateTermsAndConditions();

                return RedirectToAction("IndexSelector", "Base");
            }

            return RedirectToAction("Index", new {returnUrl});
        }

    }
}
