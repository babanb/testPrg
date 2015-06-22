﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Profile
{
    public class PlanConfirmationViewModel
    {
        public PlanConfirmationViewModel(PlanRenewalViewModel renewalPlan, PlanRenewalViewModel myPlan, PlanBillingViewModel billingInfo, string email)
        {
            //Renew Plan Info
            Promocode = renewalPlan.Promocode;
            PlanName = renewalPlan.PlanName;
            BasicPlanName = renewalPlan.BasicPlan;
            Price = renewalPlan.Price ?? 0;
            //FinalplanName = myPlan.FinalplanName;
            FinalplanName = DomainHelper.GetCurrency() + renewalPlan.Price;
            Description = renewalPlan.Description;
            AdditionalPets = renewalPlan.MaxPetCount;
            AdditionalInfo = renewalPlan.AdditionalInfo;
            StartDate = renewalPlan.StartDate;
            EndDate = renewalPlan.EndDate;
            Duration = renewalPlan.Duration;
            RemainingDays = renewalPlan.RemainingDays;
            DeletedPets = renewalPlan.DeletedPets;
            DeletedUnUsedPets = renewalPlan.DeletedUnUsedPets;
            Email = email;

            //Billing Info
            CreditCardType = billingInfo.CreditCardType;
            CreditCardNumber = billingInfo.CreditCardNumber;
            BillingExpirationDate = billingInfo.ExpirationDate;
            CVV = billingInfo.CVV;
            BillingAddress1 = billingInfo.BillingAddress1;
            BillingAddress2 = billingInfo.BillingAddress2;
            BillingCity = billingInfo.BillingCity;
            BillingState = billingInfo.BillingState;
            BillingCountry = billingInfo.BillingCountry;
            BillingZip = billingInfo.BillingZip;

            //current Plan Info
            CurrentPromocode = myPlan.Promocode;
            CurrentPlanName = myPlan.PlanName;
            CurrentBasicPlanName = myPlan.PlanName;
            CurrentPrice = Convert.ToDecimal(myPlan.Price);
            CurrentFinalplanName = DomainHelper.GetCurrency() + myPlan.Price;
            CurrentDescription = myPlan.Description;
            CurrentAdditionalPets = myPlan.AdditionalPetRenewal;
            CurrentAdditionalInfo = myPlan.AdditionalInfo;
            CurrentStartDate = myPlan.StartDate;
            CurrentEndDate = myPlan.EndDate;
            CurrentDuration = myPlan.Duration;
            CurrentRemainingDays = myPlan.RemainingDays;
        }

        public PlanConfirmationViewModel(PlanAddPetsViewModel myPlan, PlanBillingViewModel billingInfo, string email)
        {
            //Plan Info
            Promocode = myPlan.Promocode;
            PlanName = myPlan.PlanName;
            BasicPlanName = myPlan.BasicPan;
            Price = myPlan.PriceToPay;
            FinalplanName = DomainHelper.GetCurrency() + myPlan.PriceToPay;
            Description = myPlan.Description;
            AdditionalPets = myPlan.MaxPetCount;
            AdditionalInfo = myPlan.AdditionalInfo;
            StartDate = myPlan.StartDate;
            EndDate = myPlan.EndDate;
            Duration = myPlan.Duration;
            RemainingDays = myPlan.RemainingDays;
            Email = email;

            //Billing Info
            CreditCardType = billingInfo.CreditCardType;
            CreditCardNumber = billingInfo.CreditCardNumber;
            BillingExpirationDate = billingInfo.ExpirationDate;
            CVV = billingInfo.CVV;
            BillingAddress1 = billingInfo.BillingAddress1;
            BillingAddress2 = billingInfo.BillingAddress2;
            BillingCity = billingInfo.BillingCity;
            BillingState = billingInfo.BillingState;
            BillingCountry = billingInfo.BillingCountry;
            BillingZip = billingInfo.BillingZip;

        }

        public PlanConfirmationViewModel(PlanUpgradeViewModel upgradePlan, PlanBillingViewModel billingInfo, string email)
        {
            //Plan Info
            Promocode = upgradePlan.Promocode;
            PlanName = upgradePlan.PlanName;
            BasicPlanName = upgradePlan.BasicPlan;
            Price = upgradePlan.Price ?? 0;
            //FinalplanName = upgradePlan.FinalplanName;
            FinalplanName = DomainHelper.GetCurrency() + upgradePlan.Price;
            Description = upgradePlan.Description;
            AdditionalPets = upgradePlan.MaxPetCount;
            AdditionalInfo = upgradePlan.AdditionalInfo;
            StartDate = upgradePlan.StartDate;
            EndDate = upgradePlan.EndDate;
            Duration = upgradePlan.Duration;
            RemainingDays = upgradePlan.RemainingDays;
            Email = email;

            //Billing Info
            CreditCardType = billingInfo.CreditCardType;
            CreditCardNumber = billingInfo.CreditCardNumber;
            BillingExpirationDate = billingInfo.ExpirationDate;
            CVV = billingInfo.CVV;
            BillingAddress1 = billingInfo.BillingAddress1;
            BillingAddress2 = billingInfo.BillingAddress2;
            BillingCity = billingInfo.BillingCity;
            BillingState = billingInfo.BillingState;
            BillingCountry = billingInfo.BillingCountry;
            BillingZip = billingInfo.BillingZip;

        }

        public PlanConfirmationViewModel(PlanRenewalViewModel renewalPlan, PlanBillingViewModel billingInfo, string email)
        {
            //Plan Info
            Promocode = renewalPlan.Promocode;
            PlanName = renewalPlan.PlanName;
            BasicPlanName = renewalPlan.BasicPlan;
            Price = renewalPlan.Price ?? 0;
            //FinalplanName = myPlan.FinalplanName;
            FinalplanName = DomainHelper.GetCurrency() + renewalPlan.Price;
            Description = renewalPlan.Description;
            AdditionalPets = renewalPlan.MaxPetCount;
            AdditionalInfo = renewalPlan.AdditionalInfo;
            StartDate = renewalPlan.StartDate;
            EndDate = renewalPlan.EndDate;
            Duration = renewalPlan.Duration;
            RemainingDays = renewalPlan.RemainingDays;
            DeletedPets = renewalPlan.DeletedPets;
            DeletedUnUsedPets = renewalPlan.DeletedUnUsedPets;
            Email = email;

            //Billing Info
            CreditCardType = billingInfo.CreditCardType;
            CreditCardNumber = billingInfo.CreditCardNumber;
            BillingExpirationDate = billingInfo.ExpirationDate;
            CVV = billingInfo.CVV;
            BillingAddress1 = billingInfo.BillingAddress1;
            BillingAddress2 = billingInfo.BillingAddress2;
            BillingCity = billingInfo.BillingCity;
            BillingState = billingInfo.BillingState;
            BillingCountry = billingInfo.BillingCountry;
            BillingZip = billingInfo.BillingZip;
        }

        //new constructor for new renew
        public PlanConfirmationViewModel()
        { }
        //    //Plan Info
        //    Promocode = renewalPlan.Promocode;
        //    PlanName = renewalPlan.PlanName;
        //    BasicPlanName = renewalPlan.BasicPlan;
        //    Price = renewalPlan.Price ?? 0;
        //    //FinalplanName = myPlan.FinalplanName;
        //    FinalplanName = DomainHelper.GetCurrency() + renewalPlan.Price;
        //    Description = renewalPlan.Description;
        //    AdditionalPets = renewalPlan.MaxPetCount;
        //    AdditionalInfo = renewalPlan.AdditionalInfo;
        //    StartDate = renewalPlan.StartDate;
        //    EndDate = renewalPlan.EndDate;
        //    Duration = renewalPlan.Duration;
        //    RemainingDays = renewalPlan.RemainingDays;
        //    DeletedPets = renewalPlan.DeletedPets;
        //    DeletedUnUsedPets = renewalPlan.DeletedUnUsedPets;
        //    Email = email;

        //    //Billing Info
        //    CreditCardType = billingInfo.CreditCardType;
        //    CreditCardNumber = billingInfo.CreditCardNumber;
        //    BillingExpirationDate = billingInfo.ExpirationDate;
        //    CVV = billingInfo.CVV;
        //    BillingAddress1 = billingInfo.BillingAddress1;
        //    BillingAddress2 = billingInfo.BillingAddress2;
        //    BillingCity = billingInfo.BillingCity;
        //    BillingState = billingInfo.BillingState;
        //    BillingCountry = billingInfo.BillingCountry;
        //    BillingZip = billingInfo.BillingZip;

        //}

        public int PlanID { get; set; }

        [Display(Name = "Profile_PlanEdit_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        public string FinalplanName { get; set; }

        public string Email { get; set; }

        public string BasicPlanName { get; set; }

        [Display(Name = "Profile_PlanEdit_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalPets", ResourceType = typeof(Wording))]
        public int AdditionalPets { get; set; }

        [Display(Name = "Profile_PlanEdit_Price", ResourceType = typeof(Wording))]
        public decimal Price { get; set; }

        [Display(Name = "Profile_PlanEdit_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanEdit_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Profile_PlanEdit_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Profile_PlanEdit_RemainingDays", ResourceType = typeof(Wording))]
        public int RemainingDays { get; set; }

        [Display(Name = "Profile_PlanEdit_CreditCardType", ResourceType = typeof(Wording))]
        public CreditCardTypeEnum? CreditCardType { get; set; }

        [Display(Name = "Profile_PlanEdit_CreditCardNumber", ResourceType = typeof(Wording))]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Profile_PlanEdit_ExpirationDate", ResourceType = typeof(Wording))]
        public string BillingExpirationDate { get; set; }

        [Display(Name = "Profile_PlanEdit_CVV", ResourceType = typeof(Wording))]
        public int? CVV { get; set; }

        [Display(Name = "Profile_PlanEdit_Address1", ResourceType = typeof(Wording))]
        public string BillingAddress1 { get; set; }

        [Display(Name = "Profile_PlanEdit_Address2", ResourceType = typeof(Wording))]
        public string BillingAddress2 { get; set; }

        [Display(Name = "Profile_PlanEdit_City", ResourceType = typeof(Wording))]
        public string BillingCity { get; set; }

        [Display(Name = "Profile_PlanEdit_Country", ResourceType = typeof(Wording))]
        public CountryEnum? BillingCountry { get; set; }

        [Display(Name = "Profile_PlanEdit_State", ResourceType = typeof(Wording))]
        public StateEnum? BillingState { get; set; }

        [Display(Name = "Profile_PlanEdit_Zip", ResourceType = typeof(Wording))]
        public int? BillingZip { get; set; }

        public string DeletedPets { get; set; }
        public int DeletedUnUsedPets { get; set; }

        #region ForCurrentPlan details for multiple plan

        [Display(Name = "Profile_PlanEdit_Promocode", ResourceType = typeof(Wording))]
        public string CurrentPromocode { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string CurrentPlanName { get; set; }

        public string CurrentBasicPlanName { get; set; }

        [Display(Name = "Profile_PlanEdit_Price", ResourceType = typeof(Wording))]
        public decimal CurrentPrice { get; set; }

        public string CurrentFinalplanName { get; set; }

        [Display(Name = "Profile_PlanEdit_Description", ResourceType = typeof(Wording))]
        public string CurrentDescription { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalInfo", ResourceType = typeof(Wording))]
        public string CurrentAdditionalInfo { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalPets", ResourceType = typeof(Wording))]
        public int CurrentAdditionalPets { get; set; }

        [Display(Name = "Profile_PlanEdit_StartDate", ResourceType = typeof(Wording))]
        public DateTime CurrentStartDate { get; set; }

        [Display(Name = "Profile_PlanEdit_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime CurrentEndDate { get; set; }

        [Display(Name = "Profile_PlanEdit_Duration", ResourceType = typeof(Wording))]
        public int CurrentDuration { get; set; }

        [Display(Name = "Profile_PlanEdit_RemainingDays", ResourceType = typeof(Wording))]
        public int CurrentRemainingDays { get; set; }


        #endregion


        public UserSubscriptionHistory Map(PlanAddPetsViewModel planDetails, PaymentResult paymentResult, UserSubscription userSubscription, int userId)
        {
            if (userSubscription.SubscriptionService == null)
            {
                userSubscription.SubscriptionService = new SubscriptionService
                {
                    AditionalPetCount = planDetails.AdditionalPets
                };
            }
            else
            {
                userSubscription.SubscriptionService.AditionalPetCount += planDetails.AdditionalPets;
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
                UserId = userId
            };

            var billingInfo = new BillingInformation
            {
                PaymentTypeId = PaymentTypeEnum.Yearly,
                Address1 = BillingAddress1,
                Address2 = BillingAddress2,
                City = BillingCity,
                CountryId = BillingCountry.Value,
                StateId = BillingState,
                Zip = BillingZip.ToString(),
                CreditCardTypeId = CreditCardType
            };

            var paymentHistory = new PaymentHistory
            {
                Amount = Price,
                PaymentDate = DateTime.Today,
                TransactionNumber = paymentResult.OrderId,
                ErrorMessage = paymentResult.ErrorMessage,
                BillingInformation = billingInfo
            };

            userSubscription.StartDate = planDetails.StartDate;
            userSubscription.RenewalDate = planDetails.EndDate;
            userSubscription.SubscriptionId = planDetails.PlanId;
            userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };

            return subscriptionHistory;
        }

        public UserSubscriptionHistory Map(PlanUpgradeViewModel planDetails, PaymentResult paymentResult, UserSubscription userSubscription, int userId)
        {
            int oldAditionalPetCount = 0;
            if (planDetails.StartDate > DateTime.Today)
            {
                userSubscription.TempUserSubscription = new TempUserSubscription
                {
                    AditionalPetCount = planDetails.AdditionalPets,
                    StartDate = planDetails.StartDate,
                    RenewalDate = planDetails.EndDate,
                    SubscriptionId = planDetails.PlanID,
                    SubscriptionExpirationAlertId = 1,
                    SubscriptionMailSent = true
                };

                oldAditionalPetCount = userSubscription.SubscriptionService != null
                   ? userSubscription.SubscriptionService.AditionalPetCount.Value
                   : 0;
            }
            else
            {
                if (userSubscription.SubscriptionService == null)
                {
                    userSubscription.SubscriptionService = new SubscriptionService
                    {
                        AditionalPetCount = planDetails.AdditionalPets
                    };
                }
                else
                {
                    oldAditionalPetCount = userSubscription.SubscriptionService.AditionalPetCount.HasValue
                        ? userSubscription.SubscriptionService.AditionalPetCount.Value
                        : 0;
                    userSubscription.SubscriptionService.AditionalPetCount = planDetails.AdditionalPets;
                }

                userSubscription.StartDate = planDetails.StartDate;
                userSubscription.RenewalDate = planDetails.EndDate;
                userSubscription.SubscriptionId = planDetails.PlanID;

            }

            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = planDetails.PlanID,
                StartDate = planDetails.StartDate,
                EndDate = planDetails.EndDate,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = planDetails.EndDate,
                AditionalPetCount = planDetails.AdditionalPets - oldAditionalPetCount, //number of pets added in this upgrade
                UserId = userId
            };

            var billingInfo = new BillingInformation
            {
                PaymentTypeId = PaymentTypeEnum.Yearly,
                Address1 = BillingAddress1,
                Address2 = BillingAddress2,
                City = BillingCity,
                CountryId = BillingCountry.Value,
                StateId = BillingState,
                Zip = BillingZip.ToString(),
                CreditCardTypeId = CreditCardType
            };

            var paymentHistory = new PaymentHistory
            {
                Amount = Price,
                PaymentDate = DateTime.Today,
                TransactionNumber = paymentResult.OrderId,
                ErrorMessage = paymentResult.ErrorMessage,
                BillingInformation = billingInfo
            };

            userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };

            return subscriptionHistory;
        }


        public UserSubscriptionHistory Map(PlanRenewalViewModel planDetails, PaymentResult paymentResult, UserSubscription userSubscription, int userId)
        {
            int oldAditionalPetCount = 0;
            if (planDetails.StartDate > DateTime.Today)
            {
                userSubscription.TempUserSubscription = new TempUserSubscription
               {
                   AditionalPetCount = planDetails.AdditionalPetRenewal,
                   StartDate = planDetails.StartDate,
                   RenewalDate = planDetails.EndDate,
                   SubscriptionId = planDetails.PlanID,
                   SubscriptionExpirationAlertId = 1,
                   SubscriptionMailSent = true
               };

                oldAditionalPetCount = userSubscription.SubscriptionService != null
                    ? userSubscription.SubscriptionService.AditionalPetCount != null ? userSubscription.SubscriptionService.AditionalPetCount.Value
                : 0 : 0;
            }
            else
            {
                if (userSubscription.SubscriptionService == null)
                {
                    userSubscription.SubscriptionService = new SubscriptionService
                    {
                        AditionalPetCount = planDetails.AdditionalPetRenewal
                    };
                }
                else
                {
                    oldAditionalPetCount = userSubscription.SubscriptionService.AditionalPetCount.HasValue
                        ? userSubscription.SubscriptionService.AditionalPetCount != null ? userSubscription.SubscriptionService.AditionalPetCount.Value
                        : 0 : 0;
                    userSubscription.SubscriptionService.AditionalPetCount = planDetails.AdditionalPetRenewal;
                }

                userSubscription.StartDate = planDetails.StartDate;
                userSubscription.RenewalDate = planDetails.EndDate;
                userSubscription.SubscriptionId = planDetails.PlanID;

            }


            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = planDetails.PlanID,
                StartDate = planDetails.StartDate,
                EndDate = planDetails.EndDate,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = planDetails.EndDate,
                AditionalPetCount = planDetails.AdditionalPetRenewal - oldAditionalPetCount, //number of pets added in this upgrade
                UserId = userId
            };

            var billingInfo = new BillingInformation
            {
                PaymentTypeId = PaymentTypeEnum.Yearly,
                Address1 = BillingAddress1,
                Address2 = BillingAddress2,
                City = BillingCity,
                CountryId = BillingCountry.Value,
                StateId = BillingState,
                Zip = BillingZip.ToString(),
                CreditCardTypeId = CreditCardType
            };

            var paymentHistory = new PaymentHistory
            {
                Amount = Price,
                PaymentDate = DateTime.Today,
                TransactionNumber = paymentResult.OrderId,
                ErrorMessage = paymentResult.ErrorMessage,
                BillingInformation = billingInfo
            };


            userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };

            return subscriptionHistory;
        }



        public UserSubscriptionHistory Map(DateTime startDate, DateTime expirationDate, int aditionalPets, int subscriptionId, PaymentResult paymentResult, UserSubscription userSubscription, int userId)
        {
            int oldAditionalPetCount = 0;
            if (userSubscription.SubscriptionService == null)
            {
                userSubscription.SubscriptionService = new SubscriptionService
                {
                    AditionalPetCount = aditionalPets
                };
            }
            else
            {
                oldAditionalPetCount = userSubscription.SubscriptionService.AditionalPetCount.HasValue
                    ? userSubscription.SubscriptionService.AditionalPetCount.Value
                    : 0;
                userSubscription.SubscriptionService.AditionalPetCount = aditionalPets;
            }

            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = subscriptionId,
                StartDate = startDate,
                EndDate = expirationDate,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = expirationDate,
                AditionalPetCount = aditionalPets - oldAditionalPetCount,
                UserId = userId
            };

            var billingInfo = new BillingInformation
            {
                PaymentTypeId = PaymentTypeEnum.Yearly,
                Address1 = BillingAddress1,
                Address2 = BillingAddress2,
                City = BillingCity,
                CountryId = BillingCountry.Value,
                StateId = BillingState,
                Zip = BillingZip.ToString(),
                CreditCardTypeId = CreditCardType
            };

            var paymentHistory = new PaymentHistory
            {
                Amount = Price,
                PaymentDate = DateTime.Today,
                TransactionNumber = paymentResult.OrderId,
                ErrorMessage = paymentResult.ErrorMessage,
                BillingInformation = billingInfo
            };

            userSubscription.StartDate = startDate;
            userSubscription.RenewalDate = expirationDate;
            userSubscription.SubscriptionId = subscriptionId;
            userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };

            return subscriptionHistory;
        }
    }
}