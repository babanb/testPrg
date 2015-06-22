using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
namespace ADOPets.Web.ViewModels.PlansAndPromo
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(Subscription subscription, Subscription baseSubscription)
        {
            PlanID = subscription.Id;
            PlanName = subscription.Name;
            PromoCode = subscription.PromotionCode;
            PlanatRenewal = (baseSubscription != null) ? baseSubscription.Name.ToString() : "";
            IsVisibleToOwner = subscription.IsVisibleToOwner;
            IsTrial = subscription.IsTrial ?? false;
        }

        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public string PromoCode { get; set; }
        public string PlanatRenewal { get; set; }
        public string GroupOrCompany { get; set; }
        public bool IsVisibleToOwner { get; set; }
        public bool IsTrial { get; set; }
    }
}