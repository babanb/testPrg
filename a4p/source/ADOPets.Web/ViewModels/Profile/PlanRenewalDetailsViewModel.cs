﻿using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Profile
{
    public class PlanRenewalDetailsViewModel
    {

        public PlanRenewalDetailsViewModel(Model.TempUserSubscription renewalDetails, string planName, string finalPlan, int additionPets)
        {
            var subscription = renewalDetails.Subscription;
            PlanName = planName;
            FinalplanName = finalPlan;
            Promocode = subscription.PromotionCode;
            AdditionalInfo = subscription.AditionalInfo;
            Description = subscription.Description;
            NumberOfPets = subscription.MaxPetCount;
            Price = subscription.Amount;
            StartDate = renewalDetails.StartDate;
            EndDate = renewalDetails.RenewalDate ?? DateTime.Today;
            Duration = subscription.Duration == 0
                ? (int)(EndDate.Subtract(StartDate).TotalDays) + 1
                : subscription.Duration;

        }
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

        public string FinalplanName { get; set; }
    }
}