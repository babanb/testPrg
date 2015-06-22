using ADOPets.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.PlansAndPromo
{
    public class PlanRenewUpgradeViewModel
    {
        public PlanRenewUpgradeViewModel() { }

        [Display(Name = "PlanAndPromo_Add_PlanTitle", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_PlanNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public int PlanId { get; set; }

        [Display(Name = "PlanAndPromo_Add_PromocodeTitle", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_PromocodeRequired", ErrorMessageResourceType = typeof(Wording))]
        public int PromoCodeId { get; set; }

        [Display(Name = "PlansAndPromo_RenewUpgrade_SelectPlanForRenew", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_PlanatRenewalRequired", ErrorMessageResourceType = typeof(Wording))]
        public int PlanatRenewalId { get; set; }
    }
}