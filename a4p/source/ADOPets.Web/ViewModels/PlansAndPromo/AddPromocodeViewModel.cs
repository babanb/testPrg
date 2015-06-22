using ADOPets.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADOPets.Web.ViewModels.PlansAndPromo
{
    public class AddPromocodeViewModel
    {
        [Display(Name = "PlanAndPromo_Add_PromocodeTitle", ResourceType = typeof(Wording))]
        [Remote("IsPromocodeExists", "PlansAndPromo", HttpMethod = "POST", ErrorMessageResourceName = "PlansAndPromo_AddPromo_PromocodeExists", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_PromocodeRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "PetContact_Add_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PetContact_Add_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "PlansAndPromo_AddPromo_CompanyName", ResourceType = typeof(Wording))]
        public string CompanyName { get; set; }

        [Display(Name = "PlansAndPromo_AddPromo_SalesPerson", ResourceType = typeof(Wording))]
        public string SalesPerson { get; set; }

        [Display(Name = "PlansAndPromo_AddPromo_MaxOwner", ResourceType = typeof(Wording))]
        public int? MaxOwner { get; set; }

        [Display(Name = "PlansAndPromo_AddPromo_IsVisibleToOwner", ResourceType = typeof(Wording))]
        public bool IsVisibleToOwner { get; set; }

        public List<IndexPromoCode> ListPlans { get; set; }

    }

    public class IndexPromoCode
    {
        public IndexPromoCode() { }

        public IndexPromoCode(Model.Subscription subscription)
        {
            PlanId = subscription.Id;
            PlanName = subscription.Name;
            AdditionalPetInfo = subscription.AditionalInfo;
            IsCheckedId = PlanId;
        }
        public IndexPromoCode(int id, string Name, string AdditionalInfo, int IscheckedId)
        {
            PlanId = id;
            PlanName = Name;
            AdditionalPetInfo = AdditionalInfo;
            IsCheckedId = IscheckedId;
        }

        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string AdditionalPetInfo { get; set; }
        public bool IsChecked { get; set; }
        public int IsCheckedId { get; set; }
    }
}