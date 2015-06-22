﻿using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using System.Collections.Generic;

namespace ADOPets.Web.ViewModels.Profile
{
    public class PlanUpgradeViewModel
    {
        public PlanUpgradeViewModel()
        {

        }
        public bool IsFreeUser { get; set; }
        public int PlanID { get; set; }

        [Display(Name = "Profile_PlanEdit_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string BasicPlan { get; set; }

        [Display(Name = "Profile_PlanEdit_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Profile_PlanEdit_NumberOfPets", ResourceType = typeof(Wording))]
        public string NumberOfPets { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalPets", ResourceType = typeof(Wording))]
        public int AdditionalPets { get; set; }

        [Display(Name = "Profile_PlanEdit_Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }

        [Display(Name = "Profile_PlanEdit_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanEdit_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Profile_PlanEdit_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Profile_PlanEdit_RemainingDays", ResourceType = typeof(Wording))]
        public int RemainingDays { get; set; }

        public int MaxPetCount { get; set; }
        public string FinalplanName { get; set; }
        public string Myplan { get; set; }
        public int? PlanTypeId { get; set; }

        public List<SelectPlanViewModel> SelectPlan { get; set; }
    }
}