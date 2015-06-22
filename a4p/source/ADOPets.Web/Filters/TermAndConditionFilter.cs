using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ADOPets.Web.Common.Authentication;

namespace ADOPets.Web.Filters
{
    public class TermAndConditionFilter : ActionFilterAttribute
    {
        private static Dictionary<string, List<string>> whiteList = new Dictionary<string, List<string>>
        {
            {"Profile", new List<string> {"ForceToChangePassword","OwnerDetail","GetPetsPrice","GetAdditionalPetPrice","GetStartDate","GetPlanPrice","GetPlansByPromocode","GetTotalPets","GetStates","PlanRenewal","PlanConfirmation","PlanBilling","PlanUpgrade","CmcicPlanConfirmation","CmcicPlanError"}},
            {"OwnerPayment",new List<string> {"ShowMultiplePlanDetails","MultiplePlanBilling","MultiplePlanConfirmation","CancelPlan","PlanPaymentResult","Billing","Confirmation","PlanBilling","PlanConfirmation","CmcicAccountConfirmation","CmcicAccountError","CmcicPlanConfirmation","CmcicPlanError","CmcicPaymentConfirmation","CmcicPaymentError"}},
            {"TermAndCondition", new List<string>()},
            {"Payment", new List<string>()},
            {"Account", new List<string> {"Login", "ForgotPassword", "LogOut","GetStates"}},
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
                //Check if the current user has acepted the t&c
                var accepted = ctx.User.HasAcceptedTermAndConditions();
                if (!accepted)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            {"controller", "TermAndCondition"},
                            {"action", "Index"},
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