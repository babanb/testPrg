using System.Web.Mvc;
using ADOPets.Web.Filters;

namespace ADOPets.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new SessionExpireFilter());

            filters.Add(new AuthorizeAttribute(), 1);
            filters.Add(new OwnerPaymentFilter(), 2);
            filters.Add(new ChangePasswordFilter(), 3);
            filters.Add(new TermAndConditionFilter(), 4);
            filters.Add(new UpgradeOrRenewPlanFilter(), 5);
        }
    }
}