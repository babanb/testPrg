using ADOPets.Web.Resources;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Users
{
    public class AddPetsViewModel
    {
        public AddPetsViewModel()
        {

        }
        public AddPetsViewModel(Model.User user, string planName, string finalPlan, int additionPets)
        {
            var subscription = user.UserSubscription.Subscription;
            var startDate = user.UserSubscription.StartDate ?? DateTime.Now;
            var endDate = user.UserSubscription.RenewalDate ?? DateTime.Now;
            var duration = subscription.Duration == 0
                ? (int)(endDate.Subtract(startDate).TotalDays) + 1
                : subscription.Duration;
            var remainingDays = (int)(endDate - DateTime.Today).TotalDays < 0 ? 0 : (int)(endDate - DateTime.Today).TotalDays;
            remainingDays = remainingDays == 0 ? remainingDays : remainingDays + 1;

            UserID = user.Id;
            PlanId = subscription.Id;
            Promocode = subscription.PromotionCode;
            PlanName = finalPlan;
            BasicPan = planName;
            Description = subscription.Description;
            AdditionalInfo = subscription.AditionalInfo;
            AdditionalPets = additionPets;
            NumberOfPets = additionPets;
            Price = finalPlan;
            StartDate = startDate;
            EndDate = endDate;
            RemainingDays = remainingDays;
            Duration = duration;
            MaxPetCount = subscription.MaxPetCount;
            UserSubscriptionId = user.UserSubscriptionId ?? 0;
        }

        public int PlanId { get; set; }
        public int UserID { get; set; }
        public int UserSubscriptionId { get; set; }

        [Display(Name = "Profile_PlanEdit_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string BasicPan { get; set; }

        [Display(Name = "Profile_PlanEdit_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        public int NumberOfPets { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalPets", ResourceType = typeof(Wording))]
        public int AdditionalPets { get; set; }

        [Display(Name = "Profile_PlanEdit_Price", ResourceType = typeof(Wording))]
        public string Price { get; set; }

        [Display(Name = "Profile_PlanRenewal_PriceToPay", ResourceType = typeof(Wording))]
        public decimal PriceToPay { get; set; }

        [Display(Name = "Profile_PlanEdit_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanEdit_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Profile_PlanEdit_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Profile_PlanEdit_RemainingDays", ResourceType = typeof(Wording))]
        public int RemainingDays { get; set; }

        public int MaxPetCount { get; set; }
        public string Myplan { get; set; }

        public UserSubscriptionHistory Map(AddPetsViewModel planDetails, UserSubscription userSubscription, bool flagPayment)
        {
            if (!flagPayment)
            {
                userSubscription.ispaymentDone = false;
                if (userSubscription.SubscriptionService == null)
                {
                    userSubscription.SubscriptionService = new SubscriptionService
                    {
                        AditionalPetCount = planDetails.AdditionalPets - planDetails.NumberOfPets
                    };
                }
                else
                {
                    if (userSubscription.SubscriptionService.AditionalPetCount == null)
                    {
                        userSubscription.SubscriptionService.AditionalPetCount = planDetails.AdditionalPets - planDetails.NumberOfPets;
                    }
                    else
                    {
                        userSubscription.SubscriptionService.AditionalPetCount += planDetails.AdditionalPets - planDetails.NumberOfPets;
                    }

                }

            }
            else
            {
                userSubscription.ispaymentDone = false;
                userSubscription.IsAdditionalPet = true;
                userSubscription.AdditionalPetcount = planDetails.AdditionalPets - planDetails.NumberOfPets;
            }
            var subscriptionHistory = new UserSubscriptionHistory
                {
                    SubscriptionId = planDetails.PlanId,
                    StartDate = planDetails.StartDate,
                    EndDate = planDetails.EndDate,
                    PurchaseDate = DateTime.Today,
                    UsedDate = DateTime.Today,
                    RenewvalDate = planDetails.EndDate,
                    AditionalPetCount = planDetails.AdditionalPets,
                    UserId = UserID
                };
            return subscriptionHistory;
        }
    }
}