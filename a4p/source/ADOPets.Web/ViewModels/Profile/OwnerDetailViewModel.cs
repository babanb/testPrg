﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Resources;
using Model;
using ADOPets.Web.Common.Helpers;
using System.Collections.Generic;

namespace ADOPets.Web.ViewModels.Profile
{
    public class OwnerDetailViewModel
    {
        public OwnerDetailViewModel(User user, Model.Subscription subscription, Model.Subscription subscriptionRenewal, List<Profile.SelectPlanViewModel> SelectPlanList)
        {
            IsFreeUser = false;
            OwnerId = user.Id;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            BirthDate = !String.IsNullOrEmpty(user.BirthDate)
                ? Convert.ToDateTime(user.BirthDate, CultureInfo.InvariantCulture).ToShortDateString()
                : String.Empty;
            Gender = user.GenderId;
            Email = user.Email;
            Address1 = user.Address1;
            Address2 = user.Address2;
            City = user.City;
            Country = user.CountryId;
            State = user.StateId;
            Zip = user.PostalCode;
            PrimaryPhone = user.PrimaryPhone;
            SecondaryPhone = user.SecondaryPhone;
            TimeZone = user.TimeZoneId;
            Image = user.ProfileImage;
            IsSearchable = user.IsNonSearchable != true;
            CreationDate = !string.IsNullOrEmpty(user.CreationDate.ToString())
            ? (DateTime?)Convert.ToDateTime(user.CreationDate, CultureInfo.InvariantCulture)
            : null;

            var additionalPets = user.UserSubscription.SubscriptionService == null ? 0 : user.UserSubscription.SubscriptionService.AditionalPetCount ?? 0;
            var totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * additionalPets);
            var totalMra = additionalPets + subscription.MRACount;
            var totalPets = additionalPets + subscription.MaxPetCount;
            var totalSmo = subscription.SMOCount;
            var finalPlan1 = GetPlanName(subscription, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);

            var DefaultplanName = string.Format(Resources.Wording.Account_SignUp_PetsInformation, subscription.Amount, subscription.MaxPetCount, subscription.MaxPetCount);
            var additionalInfo = subscription.AditionalInfo == null ? string.Empty : string.Format(Resources.Wording.Account_Signup_AdditionalPetInfo, subscription.AmmountPerAddionalPet);
            var planId = subscription.Id;
            var PlanName = subscription.Name;



            // var subscription = user.UserSubscription.Subscription;
            // var subscription = user.UserSubscription.Subscription;
            var startDate = user.UserSubscription.StartDate ?? DateTime.Now;
            var endDate = user.UserSubscription.RenewalDate ?? DateTime.Now;
            var freeACUpgradeDate = user.UserSubscription.RenewalDate ?? DateTime.Now;
            var duration = subscription.Duration == 0
                ? (int)(endDate.Subtract(startDate).TotalDays) + 1
                : subscription.Duration;
            var remainingDays = (int)(endDate - DateTime.Today).TotalDays < 0 ? 0 : (int)(endDate - DateTime.Today).TotalDays;
            remainingDays = remainingDays == 0 ? remainingDays : remainingDays + 1;

            var FreeACDuration = duration;
            var FreeACRemainingdays = remainingDays;

            MyPlan = new PlanDetailViewModel();
            MyPlan.PlanName = PlanName;
            MyPlan.FinalplanName = PlanName;
               // subscription.IsTrial == true ? DefaultplanName : finalPlan1;
            MyPlan.Promocode = subscription.PromotionCode;
           // MyPlan.AdditionalInfo = additionalInfo;
            MyPlan.Description = subscription.Description;
            MyPlan.NumberOfPets = subscription.MaxPetCount;
            MyPlan.Price = subscription.Amount;
            MyPlan.StartDate = startDate;
            MyPlan.EndDate = endDate;
            MyPlan.Duration = duration;
            MyPlan.RemainingDays = remainingDays;
            MyPlan.isTrial = subscription.IsTrial ?? false;
            MyPlan.isRenewed = user.UserSubscription.TempUserSubscription != null ? true : false;
            MyPlan.IsFreeUser = false;
            if (subscription.IsBase && subscription.PlanTypeId == PlanTypeEnum.BasicFree && subscription.Amount == null)
            {
                MyPlan.IsFreeUser = true;
            
            }
            MyPlan.MaxPetcount = subscription.MaxPetCount;
            MyPlan.PlanType = (subscription.PlanTypeId == PlanTypeEnum.BasicFree) ? PlanTypeEnum.BasicFree : PlanTypeEnum.Premium;

            PlanAddPets = new PlanAddPetsViewModel
            {
                PlanId = subscription.Id,
                Promocode = subscription.PromotionCode,
                PlanName = subscription.IsTrial == true ? DefaultplanName : finalPlan1,
                BasicPan = DefaultplanName,
                Description = subscription.Description,
                AdditionalInfo = additionalInfo,
                AdditionalPets = additionalPets,
                NumberOfPets = additionalPets,
                Price = finalPlan1,
                StartDate = startDate,
                EndDate = endDate,
                RemainingDays = remainingDays,
                Duration = duration,
                MaxPetCount = subscription.MaxPetCount
            };


            var totalAmmountRenewal = subscriptionRenewal.Amount + (subscriptionRenewal.AmmountPerAddionalPet * additionalPets);
            var totalMraRenewal = additionalPets + subscriptionRenewal.MRACount;
            var totalPetsRenewal = additionalPets + subscriptionRenewal.MaxPetCount;
            var totalSmoRenewal = subscriptionRenewal.SMOCount;
            var finalPlanRenewal = GetPlanName(subscriptionRenewal, totalAmmountRenewal ?? 0, totalPetsRenewal, totalMraRenewal ?? 0, totalSmoRenewal ?? 0);

            var DefaultplanNameRenewal = string.Format(Resources.Wording.Account_SignUp_PetsInformation, subscriptionRenewal.Amount, subscriptionRenewal.MaxPetCount, subscriptionRenewal.MaxPetCount);
            var additionalInfoRenewal = subscriptionRenewal.AditionalInfo == null ? string.Empty : string.Format(Resources.Wording.Account_Signup_AdditionalPetInfo, subscriptionRenewal.AmmountPerAddionalPet);

            if (DomainHelper.GetDomain() == DomainTypeEnum.India || DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                if (subscription.PlanTypeId == PlanTypeEnum.BasicFree && subscription.IsBase && subscription.Amount == null)//==> "Free Account With Limited Access")
                {
                    additionalInfoRenewal = subscriptionRenewal.AditionalInfo;
                    freeACUpgradeDate = startDate.AddYears(1);
                    freeACUpgradeDate = freeACUpgradeDate.AddDays(-1);
                    FreeACDuration = (int)(freeACUpgradeDate.Subtract(startDate).TotalDays);
                    FreeACRemainingdays = (int)(freeACUpgradeDate - DateTime.Today).TotalDays < 0 ? 0 : (int)(freeACUpgradeDate - DateTime.Today).TotalDays;
                    FreeACRemainingdays = FreeACRemainingdays == 0 ? FreeACRemainingdays : FreeACRemainingdays + 1;

                }
            }

            UpgradePlan = new PlanUpgradeViewModel();
            UpgradePlan.PlanName = DefaultplanNameRenewal;
            UpgradePlan.FinalplanName = subscriptionRenewal.IsTrial == true ? DefaultplanNameRenewal : finalPlanRenewal;
            UpgradePlan.Promocode = subscriptionRenewal.PromotionCode;
            UpgradePlan.AdditionalInfo = additionalInfoRenewal;
            UpgradePlan.NumberOfPets = additionalPets.ToString();
            UpgradePlan.AdditionalPets = additionalPets;
            UpgradePlan.StartDate = startDate;
            UpgradePlan.EndDate = freeACUpgradeDate;
            UpgradePlan.Price = subscriptionRenewal.Amount;
            UpgradePlan.Duration = FreeACDuration;
            UpgradePlan.RemainingDays = FreeACRemainingdays;
            UpgradePlan.IsFreeUser = false;
            if (subscription.PlanType != null) { UpgradePlan.PlanTypeId = Convert.ToInt32(subscription.PlanTypeId.Value.ToString()); }
            //var SelectPlanList = new List<SelectPlanViewModel>();
            UpgradePlan.SelectPlan = SelectPlanList;
            
            RenewalPlan = new PlanRenewalViewModel();
            RenewalPlan.PlanName = DefaultplanNameRenewal;
            RenewalPlan.FinalplanName = subscriptionRenewal.IsTrial == true ? DefaultplanNameRenewal : finalPlanRenewal;
            RenewalPlan.Promocode = subscriptionRenewal.PromotionCode;
            RenewalPlan.AdditionalInfo = additionalInfoRenewal;
            RenewalPlan.NumberOfPets = additionalPets.ToString();
            RenewalPlan.AdditionalPetRenewal = additionalPets;
            RenewalPlan.StartDate = startDate;
            RenewalPlan.EndDate = endDate;
            RenewalPlan.Price = subscriptionRenewal.Amount + (subscriptionRenewal.AmmountPerAddionalPet * additionalPets);
            RenewalPlan.Duration = duration;
            RenewalPlan.RemainingDays = remainingDays;
            RenewalPlan.DeletedUnUsedPets = 0;
            RenewalPlan.MaxPetCount = subscriptionRenewal.MaxPetCount;


            if (DomainHelper.GetDomain() == DomainTypeEnum.India || DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                if (subscription.PlanTypeId == PlanTypeEnum.BasicFree && subscription.IsBase && subscription.Amount == null)//==> "Free Account With Limited Access")
                {
                    IsFreeUser = true;
                    DefaultplanName = subscription.Name;
                    additionalInfo = "";
                    MyPlan.FinalplanName = DefaultplanName;
                    finalPlan1 = DefaultplanName;
                    MyPlan.PlanName = DefaultplanName;
                    MyPlan.AdditionalInfo = additionalInfo;
                    MyPlan.IsFreeUser = true;
                    MyPlan.MaxPetcount = subscription.MaxPetCount;

                    //UpgradePlan.PlanName = DefaultplanName;
                    UpgradePlan.IsFreeUser = true;
                    UpgradePlan.StartDate = DateTime.Now;
                }
            }

            isPlanRenewed = user.UserSubscription.TempUserSubscription != null ? true : false;

        }

        public OwnerDetailViewModel(string firstName, string lastName, string email, int ownerid)
        {
            FirstName = firstName;
            LastName = lastName;
            OwnerId = ownerid;
            Email = email;
        }

        public bool IsFreeUser { get; set; }

        [Display(Name = "Profile_OwnerDetail_OwnerId", ResourceType = typeof(Wording))]
        public int OwnerId { get; set; }

        [Display(Name = "Profile_OwnerDetail_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Profile_OwnerDetail_MiddleName", ResourceType = typeof(Wording))]
        public string MiddleName { get; set; }

        [Display(Name = "Profile_OwnerDetail_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Profile_OwnerDetail_DateofBirth", ResourceType = typeof(Wording))]
        public string BirthDate { get; set; }

        [Display(Name = "Profile_OwnerEdit_CreationDate", ResourceType = typeof(Wording))]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Profile_OwnerDetail_Gender", ResourceType = typeof(Wording))]
        public GenderEnum? Gender { get; set; }

        [Display(Name = "Profile_OwnerDetail_Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Profile_OwnerDetail_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Profile_OwnerDetail_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Profile_OwnerDetail_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Profile_OwnerDetail_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Profile_OwnerDetail_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "Profile_OwnerDetail_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "Profile_OwnerDetail_PrimaryPhone", ResourceType = typeof(Wording))]
        public string PrimaryPhone { get; set; }

        [Display(Name = "Profile_OwnerDetail_SecondaryPhone", ResourceType = typeof(Wording))]
        public string SecondaryPhone { get; set; }

        [Display(Name = "Profile_OwnerDetail_TimeZone", ResourceType = typeof(Wording))]
        public TimeZoneEnum? TimeZone { get; set; }

        public byte[] Image { get; set; }

        [Display(Name = "Profile_OwnerDetail_NonSearchable", ResourceType = typeof(Wording))]
        public bool IsSearchable { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }
        public PlanDetailViewModel MyPlan { get; set; }
        public PlanAddPetsViewModel PlanAddPets { get; set; }
        public PlanRenewalViewModel RenewalPlan { get; set; }
        public PlanUpgradeViewModel UpgradePlan { get; set; }
        public bool isPlanRenewed { get; set; }

        private string GetPlanName(Model.Subscription subscription, decimal totalAmt, int totalPets, int TotalMra, int totalSmo)
        {
            //var subscriptionName = "";
            //if (subscription.HasMRA && subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithMRAWithSMO : Resources.Subscription.PlanWithMRAWithSMO, totalAmt, totalPets, TotalMra, totalSmo);
            //}
            //else if (subscription.HasMRA && !subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithMRA : Resources.Subscription.PlanWithMRA, totalAmt, totalPets, TotalMra);
            //}
            //else if (!subscription.HasMRA && subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithSMO : Resources.Subscription.PlanWithSMO, totalAmt, totalPets, totalSmo);
            //}
            //else if (!subscription.HasMRA && !subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithoutMRA : Resources.Subscription.PlanWithoutMRA, totalAmt, totalPets);
            //}
            return subscription.Name;
            }
            }
            }
