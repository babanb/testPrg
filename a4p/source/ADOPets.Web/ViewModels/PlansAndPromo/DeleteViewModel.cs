using ADOPets.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.PlansAndPromo
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(int planId) 
        {
            PlanId = planId;
        }

        public int PromocodeId { get; set; }
        public int PlanId { get; set; }
        public bool IsDeletePlan { get; set; }
        public bool IsDeletePromocode { get; set; }
    }
}