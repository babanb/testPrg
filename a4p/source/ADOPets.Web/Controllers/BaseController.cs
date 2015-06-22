using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using ADOPets.Web.Common.Helpers;
using EmailSender;
using Model;
using Repository.Implementations;
using Repository.Interfaces;
using System.Security.Principal;
using System.Web.Security;
using Constants = ADOPets.Web.Common.Constants;
using WebConfigHelper = ADOPets.Web.Common.Helpers.WebConfigHelper;

namespace ADOPets.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            UnitOfWork = new UnitOfWork();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string domain = DomainHelper.GetDomain().ToString();
            EmailSender = new MailSenderHelper(DomainHelper.GetLogoForEmailPath(), DomainHelper.GetLogoSignatureForEmailPath(), HttpContext.Server.MapPath(DomainHelper.GetMraFormPdfPath()), domain);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult IndexSelector()
        {
            if (HttpContext.User.IsInRole(UserTypeEnum.Admin.ToString()))
            {
                return RedirectToAction("Index", "Users");
            }
            else if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianExpert.ToString()))
            {
                 
                return RedirectToAction("MyAccount", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Pet");
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cultureName = Session[Constants.CurrentCultureName] as string;

            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = CultureHelper.GetDefaultCulture();
                Session[Constants.CurrentCultureName] = cultureName;
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected MailSenderHelper EmailSender { get; private set; }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }

        #region Code for Alerts Mesages (Sucees, Error, Worning etc)

        public void Success(string message)
        {
            TempData["SuccessMessage"] = message;
        }
        #endregion

    }
}
