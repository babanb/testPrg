using ADOPets.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADOPets.Web.ViewModels.PlansAndPromo
{
    public class EditPromocodeViewModel
    {

        public EditPromocodeViewModel()
        {

        }

        public EditPromocodeViewModel(Model.Subscription promocode)
        {
            PlanID = promocode.Id;
            Promocode = promocode.PromotionCode;
            StartDate = promocode.startDateValide;
            EndDate = promocode.EndDateValide;
            CompanyName = promocode.CompanyName;
            SalesPerson = promocode.SalesPerson;
            MaxOwner = promocode.MaxOwnerCount;
            IsVisibleToOwner = promocode.IsVisibleToOwner;

        }

        public int PlanID { get; set; }

        [Display(Name = "PlanAndPromo_Add_PromocodeTitle", ResourceType = typeof(Wording))]
        [Remote("IsPromocodeExistsForEdit", "PlansAndPromo", HttpMethod = "POST", ErrorMessageResourceName = "PlansAndPromo_AddPromo_PromocodeExists", ErrorMessageResourceType = typeof(Wording))]
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


        public void Map(Model.Subscription sub)
        {
            sub.MaxOwnerCount = MaxOwner ?? 0;
            sub.PromotionCode = Promocode;
            sub.startDateValide = StartDate;
            sub.EndDateValide = EndDate;
            sub.CompanyName = CompanyName;
            sub.SalesPerson = SalesPerson;
            sub.IsVisibleToOwner = IsVisibleToOwner;

        }


    }
}