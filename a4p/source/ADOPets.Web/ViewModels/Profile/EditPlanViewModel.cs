﻿using ADOPets.Web.Resources;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Profile
{
    public class EditPlanViewModel
    {

        public EditPlanViewModel()
        {

        }
        public EditPlanViewModel(Model.User user, Model.Subscription subscription, string planName, string finalPlan, int additionPets, DateTime startDate, DateTime endDate)
        {
            //var subscription = user.UserSubscription.Subscription;
            DateTime startDt = Convert.ToDateTime(user.UserSubscription.StartDate);
            var duration = subscription.Duration == 0
                ? (int)(endDate.Subtract(startDt).TotalDays) + 1
                : subscription.Duration;
            var remainingDays = (int)(endDate - startDate).TotalDays < 0 ? 0 : (int)(endDate - startDate).TotalDays;
            remainingDays = remainingDays == 0 ? remainingDays : remainingDays + 1;

            PlanName = planName;
            PlanID = subscription.Id;
            FinalplanName = finalPlan;
            Promocode = subscription.PromotionCode;
            CurrentPromocode = Promocode;
            AdditionalInfo = subscription.AditionalInfo ?? string.Empty;
            NumberOfPets = additionPets.ToString();
            AdditionalPetRenewal = additionPets;
            StartDate = startDt;
            EndDate = endDate;
            Price = subscription.Amount ?? 0;//  +(subscription.AmmountPerAddionalPet * additionPets);
            Duration = duration;
            RemainingDays = remainingDays;
            DeletedUnUsedPets = 0;
            MaxPetCount = subscription.MaxPetCount;
            UserID = user.Id;
            desc = subscription.Description;
            UserSubscriptionId = user.UserSubscriptionId ?? 0;
        }


        public int PlanID { get; set; }
        public int UserID { get; set; }
        public int UserSubscriptionId { get; set; }
        public string desc { get; set; }

        public string CurrentPromocode { get; set; }
        [Display(Name = "Profile_PlanRenewal_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Profile_PlanRenewal_PlanName", ResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        [Display(Name = "Profile_PlanRenewal_PlanName", ResourceType = typeof(Wording))]
        public string BasicPlan { get; set; }

        [Display(Name = "Profile_PlanRenewal_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Profile_PlanRenewal_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Profile_PlanRenewal_AdditionalPets", ResourceType = typeof(Wording))]
        public string NumberOfPets { get; set; }

        [Display(Name = "Profile_PlanRenewal_AdditionalPets", ResourceType = typeof(Wording))]
        public int AdditionalPetRenewal { get; set; }

        [Display(Name = "Profile_PlanRenewal_Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }

        [Display(Name = "Profile_PlanRenewal_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanRenewal_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Profile_PlanRenewal_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Profile_PlanRenewal_RemainingDays", ResourceType = typeof(Wording))]
        public int RemainingDays { get; set; }

        public int MaxPetCount { get; set; }
        public string FinalplanName { get; set; }
        public string Myplan { get; set; }
        public string DeletedPets { get; set; }
        public int DeletedUnUsedPets { get; set; }

        //public UserSubscriptionHistory Map(EditPlanViewModel planDetails, UserSubscription userSubscription)
        //{
        //    int oldAditionalPetCount = 0;

        //    //userSubscription.TempUserSubscription = new TempUserSubscription
        //    //{
        //    //    AditionalPetCount = planDetails.AdditionalPetRenewal,
        //    //    StartDate = planDetails.StartDate,
        //    //    RenewalDate = planDetails.EndDate,
        //    //    SubscriptionId = planDetails.PlanID,
        //    //    SubscriptionExpirationAlertId = 1,
        //    //    SubscriptionMailSent = true,
        //    //    ispaymentDone = false,
        //    //    deletedPetsID = planDetails.DeletedPets,
        //    //    actionName = "edit"
        //    //};

        //    oldAditionalPetCount = userSubscription.SubscriptionService != null
        //       ? userSubscription.SubscriptionService.AditionalPetCount ?? 0
        //       : 0;
        //    userSubscription.ispaymentDone = false;

        //    var subscriptionHistory = new UserSubscriptionHistory
        //    {
        //        SubscriptionId = planDetails.PlanID,
        //        StartDate = planDetails.StartDate,
        //        EndDate = planDetails.EndDate,
        //        PurchaseDate = DateTime.Today,
        //        UsedDate = DateTime.Today,
        //        RenewvalDate = planDetails.EndDate,
        //        AditionalPetCount = planDetails.AdditionalPetRenewal - oldAditionalPetCount, //number of pets added in this upgrade
        //        UserId = UserID
        //    };

        //    return subscriptionHistory;
        //}




        internal void map(UserSubscription userSubscription)
        {
            userSubscription.ispaymentDone = false;
            userSubscription.SubscriptionId = Convert.ToInt32(PlanName);
            userSubscription.SubscriptionService.AditionalPetCount = Convert.ToInt32(NumberOfPets);
        }
    }
}