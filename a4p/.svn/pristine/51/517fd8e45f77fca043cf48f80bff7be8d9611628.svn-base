using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ADOPets.Web.Filters
{
    public class SessionExpireFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                if (filterContext.HttpContext.Session.IsNewSession && filterContext.HttpContext.Request.IsAuthenticated)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        HttpContext.Current.Items["AjaxPermissionDenied"] = true;
                    }

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            {"controller", "Account"},
                            {"action", "LogOut"}
                        });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}