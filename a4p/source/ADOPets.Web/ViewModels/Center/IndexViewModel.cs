using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Center
{
    public class IndexViewModel
    {
        public IndexViewModel(Model.Center center, Model.Subscription sub)
        {
            
                CenterID = center.Id;
                if (sub != null)
                {
                    CenterName = center.CenterName;
                PromoCode = sub.PromotionCode;
                IsDeleted = center.IsDeleted;
            }
        }

        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public string PromoCode { get; set; }
        public bool IsDeleted { get; set; }
    }
}