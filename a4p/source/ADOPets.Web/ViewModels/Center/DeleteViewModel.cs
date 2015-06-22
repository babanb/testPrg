using ADOPets.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Center
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(Model.Center center, Model.Subscription sub)
        {
            Id = center.Id;
            CenterName = center.CenterName;
            Promocode = sub.PromotionCode;
        }

        public int Id { get; set; }

        [Display(Name = "Center_Index_CenterName", ResourceType = typeof(Wording))]
        public string CenterName { get; set; }

        [Display(Name = "Center_Index_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }
    }
}