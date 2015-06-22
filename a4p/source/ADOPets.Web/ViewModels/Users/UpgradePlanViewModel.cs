﻿using ADOPets.Web.Resources;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Users
{
    public class UpgradePlanViewModel
    {

        public UpgradePlanViewModel()
        {

        }
        public UpgradePlanViewModel(Model.User user, Model.Subscription subscription, string planName, string finalPlan, int additionPets, DateTime startDate, DateTime endDate, decimal? price)
        {
            var duration = subscription.Duration == 0
               ? (int)(endDate.Subtract(startDate).TotalDays) + 1
               : subscription.Duration;
            var remainingDays = (int)(endDate - startDate).TotalDays < 0 ? 0 : (int)(endDate - startDate).TotalDays;
            remainingDays = remainingDays == 0 ? remainingDays : remainingDays + 1;
            PlanName = planName;
            FinalplanName = finalPlan;
            Promocode = subscription.PromotionCode;
            AdditionalInfo = subscription.AditionalInfo ?? string.Empty;
            NumberOfPets = additionPets.ToString();
            AdditionalPets = additionPets;
            StartDate = startDate;
            EndDate = endDate;
            Price = subscription.Amount;
            Duration = duration;
            RemainingDays = remainingDays;
            UserID = user.Id;
            Desc = subscription.Description;
            UserSubscriptionId = user.UserSubscriptionId ?? 0;
            MaxPetCount = subscription.MaxPetCount;
            Price = price ?? 0;
        }

        public int PlanID { get; set; }
        public int UserID { get; set; }
        public int UserSubscriptionId { get; set; }
        public string Desc { get; set; }

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

        public UserSubscriptionHistory Map(UserSubscription userSubscription)
        {
            int oldAditionalPetCount = 0;

            userSubscription.TempUserSubscription = new TempUserSubscription
            {
                AditionalPetCount = AdditionalPets,
                StartDate = StartDate,
                RenewalDate = EndDate,
                SubscriptionId = PlanID,
                SubscriptionExpirationAlertId = 1,
                SubscriptionMailSent = true,
                ispaymentDone = false,
                actionName = "upgrade"
            };

            oldAditionalPetCount = userSubscription.SubscriptionService != null
               ? userSubscription.SubscriptionService.AditionalPetCount ?? 0
               : 0;

            userSubscription.ispaymentDone = false;
            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = PlanID,
                StartDate = StartDate,
                EndDate = EndDate,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = EndDate,
                AditionalPetCount = AdditionalPets - oldAditionalPetCount, //number of pets added in this upgrade
                UserId = UserID
            };


            return subscriptionHistory;
        }


    }
}