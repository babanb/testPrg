﻿using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Profile
{
    public class PlanDetailViewModel
    {
        public PlanDetailViewModel()
        {
        }
       


        public bool IsFreeUser { get; set; }
        public int MaxPetcount { get; set; }
        public string BasicPlan { get; set; }
        public int PlanId { get; set; }
        public bool IsBase { get; set; }
        public decimal? Amount { get; set; }
        public string description { get; set; }
        public int Dueration { get; set; }
        public int MaxPetCount { get; set; }
        public PlanTypeEnum PlanType { get; set; }
       
        public DateTime ExpirationDate { get; set; }
        
        [Display(Name = "Profile_PlanDetail_PlanName", ResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        [Display(Name = "Profile_PlanDetail_Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }

        [Display(Name = "Profile_PlanDetail_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Profile_PlanDetail_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Profile_PlanDetail_NumberOfPets", ResourceType = typeof(Wording))]
        public int NumberOfPets { get; set; }

        [Display(Name = "Profile_PlanDetail_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Profile_PlanDetail_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanDetail_EndDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Profile_PlanDetail_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Profile_PlanDetail_RemainingDays", ResourceType = typeof(Wording))]
        public int RemainingDays { get; set; }

        public string FinalplanName { get; set; }
        public bool isTrial { get; set; }
        public bool isRenewed { get; set; }
    }
}