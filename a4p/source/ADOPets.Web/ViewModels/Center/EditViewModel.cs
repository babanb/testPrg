using ADOPets.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Center
{
    public class EditViewModel
    {
        public EditViewModel()
        { }

        public EditViewModel(Model.Center center,Model.Subscription sub)
        {
            CenterID = center.Id;
            CenterName = center.CenterName;
            PromoCode = sub.PromotionCode;
        }
        public int CenterID { get; set; }
        [Display(Name = "Users_AddCenter_Name", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Center_Add_Center_NameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string CenterName { get; set; }
        [Display(Name = "PromoCodeAdd_Name", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PromoCode_Add_PromocodeRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PromoCode { get; set; }
    }
}