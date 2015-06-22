using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace ADOPets.Web.Filters
{
    public class SMOPaymentFilter : ActionFilterAttribute
    {
        private static Dictionary<string, List<string>> whiteList = new Dictionary<string, List<string>>
        {
            {"Account", new List<string> {"LogOut","GetStates"}},
            {"Profile", new List<string> {"ForceToChangePassword","OwnerDetail","GetPetsPrice","GetAdditionalPetPrice","GetStartDate","GetPlanPrice","GetPlansByPromocode","GetTotalPets","GetStates","PlanRenewal","PlanConfirmation","PlanBilling","PlanUpgrade"}},
            {"OwnerPayment",new List<string> {"ShowMultiplePlanDetails","MultiplePlanBilling","MultiplePlanConfirmation","PlanPaymentResult","Billing","Confirmation","PlanBilling","PlanConfirmation"}},
            {"Payment", new List<string>()},
            {"Message", new List<string> {"MessageNotification", "MessageCount", "GetImage"}},
            {"NewsFeed", new List<string> {"NewsFeedNotification"}},
            {"Calender", new List<string> {"CalenderNotification"}}
        };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var ctx = HttpContext.Current;

            if (ctx.Session != null && ctx.User.Identity.IsAuthenticated && !BelongsToWhiteList(controllerName, actionName))
            {
                var isNewSMOByDirector = true;
                if (!isNewSMOByDirector)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            {"controller", "OwnerPayment"},
                            {"action", "ShowMultiplePlanDetails"},
                            {"returnUrl", filterContext.HttpContext.Request.RawUrl}
                        });
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private bool BelongsToWhiteList(string controllerName, string actionName)
        {
            return whiteList.ContainsKey(controllerName) && (!whiteList[controllerName].Any() || whiteList[controllerName].Contains(actionName));
        }
    }
}