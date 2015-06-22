using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Profile
{
    public class NewPlanRenewViewModel
    {
        public NewPlanRenewViewModel()
        {

        }

        public NewPlanRenewViewModel(Model.Subscription subModel, int currPlanId = 0)
        {
            if (subModel.PlanFeatures != null && subModel.PlanFeatures.Count() > 0)
            {
                lstPlanData = new PlanDetailsViewModel(subModel.PlanFeatures.First(), subModel, currPlanId);
            }
            else
            {
                lstPlanData = new PlanDetailsViewModel(true, subModel, currPlanId);
            }
            Promocode = (subModel.IsPromotionCode) ? subModel.PromotionCode : string.Empty;
        }

        [Display(Name = "Profile_PlanRenewal_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanRenewal_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Account_SignUp_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        public PlanDetailsViewModel lstPlanData;
    }

    public class PlanDetailsViewModel
    {
        public PlanDetailsViewModel(Model.PlanFeature planFeature, Model.Subscription subModel, int currPlanId = 0)
        {
            string planTypeName = (subModel.PlanType != null) ? subModel.PlanType.Name : string.Empty;
            PlanType = planTypeName;
            Price = subModel.Amount.ToString();
            SubscriptionId = subModel.Id;
            PetCount = subModel.MaxPetCount;
            ISBase = subModel.IsBase;
            Promocode = subModel.PromotionCode;
            SubscriptionId = subModel.Id;
            PaymentType = (subModel.PaymentType != null) ? GetPaymentType(subModel.PaymentTypeId) : Resources.Wording.Account_Signup_Free;

            IsPetIDInformation = planFeature.IsPetIDInformation;
            IsEmergencyContact = planFeature.IsEmergencyContact;
            IsVetInformation = planFeature.IsVetInformation;
            IsMessage = planFeature.IsMessage;
            IsCalendar = planFeature.IsCalendar;
            IsReminder = planFeature.IsReminder;
            IsPhotoGallery = planFeature.IsPhotoGallery;
            IsPetShare = planFeature.IsPetShare;
            IsHealthHistory = planFeature.IsHealthHistory;
            IsHealthMeasureTracker = planFeature.IsHealthMeasureTracker;
            IsMedicalDocument = planFeature.IsMedicalDocument;
            IsMRA = planFeature.IsMRA;
            IsSMO = planFeature.IsSMO;
            IsEC = planFeature.IsEC;

            IsCurrentPlan = (currPlanId == subModel.Id) ? true : false;
        }

        public PlanDetailsViewModel(bool isFeature, Model.Subscription subModel, int currPlanId = 0)
        {
            string planTypeName = (subModel.PlanType != null) ? subModel.PlanType.Name : string.Empty;
            PlanType = planTypeName;
            Price = subModel.Amount.ToString();
            SubscriptionId = subModel.Id;
            PetCount = subModel.MaxPetCount;
            ISBase = subModel.IsBase;
            Promocode = subModel.PromotionCode;
            SubscriptionId = subModel.Id;
            PaymentType = (subModel.PaymentType != null) ? GetPaymentType(subModel.PaymentTypeId) : Resources.Wording.Account_Signup_Free;

            IsPetIDInformation = isFeature;
            IsEmergencyContact = isFeature;
            IsVetInformation = isFeature;
            IsMessage = isFeature;
            IsCalendar = isFeature;
            IsReminder = isFeature;
            IsPhotoGallery = isFeature;
            IsPetShare = isFeature;
            IsHealthHistory = isFeature;
            IsHealthMeasureTracker = isFeature;
            IsMedicalDocument = isFeature;
            IsMRA = isFeature;
            IsSMO = isFeature;
            IsEC = isFeature;

            IsCurrentPlan = (currPlanId == subModel.Id) ? true : false;
        }

        private string GetPaymentType(PaymentTypeEnum? PaymentTypeId)
        {
            string paymentType = string.Empty;

            if (PaymentTypeId == PaymentTypeEnum.Yearly)
            {
                paymentType = "/" + Resources.Wording.Account_Signup_Year;
            }
            else if (PaymentTypeId == PaymentTypeEnum.Monthly)
            {
                paymentType = "/" + Resources.Wording.Account_Signup_Month;
            }
            return paymentType;
        }

        public PlanDetailsViewModel()
        { }
        public bool IsCurrentPlan { get; set; }

        public string PaymentType { get; set; }

        [Display(Name = "Account_SignUp_PlanType", ResourceType = typeof(Wording))]
        public string PlanType { get; set; }

        public int PetCount { get; set; }

        public string Promocode { get; set; }

        public bool ISBase { get; set; }

        public Nullable<int> SubscriptionId { get; set; }

        [Display(Name = "Profile_PlanRenewal_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanRenewal_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Account_SignUp_Price", ResourceType = typeof(Wording))]
        public string Price { get; set; }

        [Display(Name = "Account_SignUp_IsPetIDInformation", ResourceType = typeof(Wording))]
        public bool IsPetIDInformation { get; set; }

        [Display(Name = "Account_SignUp_IsEmergencyContact", ResourceType = typeof(Wording))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "Account_SignUp_IsVetInformation", ResourceType = typeof(Wording))]
        public bool IsVetInformation { get; set; }

        [Display(Name = "Account_SignUp_IsMessage", ResourceType = typeof(Wording))]
        public bool IsMessage { get; set; }

        [Display(Name = "Account_SignUp_IsCalendar", ResourceType = typeof(Wording))]
        public bool IsCalendar { get; set; }

        [Display(Name = "Account_SignUp_IsReminder", ResourceType = typeof(Wording))]
        public bool IsReminder { get; set; }

        [Display(Name = "Account_SignUp_IsPhotoGallery", ResourceType = typeof(Wording))]
        public bool IsPhotoGallery { get; set; }

        [Display(Name = "Account_SignUp_IsPetShare", ResourceType = typeof(Wording))]
        public bool IsPetShare { get; set; }

        [Display(Name = "Account_SignUp_IsHealthHistory", ResourceType = typeof(Wording))]
        public bool IsHealthHistory { get; set; }

        [Display(Name = "Account_SignUp_IsHealthMeasureTracker", ResourceType = typeof(Wording))]
        public bool IsHealthMeasureTracker { get; set; }

        [Display(Name = "Account_SignUp_IsMedicalDocument", ResourceType = typeof(Wording))]
        public bool IsMedicalDocument { get; set; }

        [Display(Name = "Account_SignUp_IsMRA", ResourceType = typeof(Wording))]
        public bool IsMRA { get; set; }

        [Display(Name = "Account_SignUp_IsSMO", ResourceType = typeof(Wording))]
        public bool IsSMO { get; set; }

        [Display(Name = "Account_SignUp_IsEC", ResourceType = typeof(Wording))]
        public bool IsEC { get; set; }
    }
}