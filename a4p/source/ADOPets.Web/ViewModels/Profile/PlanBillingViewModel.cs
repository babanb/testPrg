﻿using System.Globalization;
using ADOPets.Web.Resources;
using Model;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using System;

namespace ADOPets.Web.ViewModels.Profile
{
    public class PlanBillingViewModel
    {
        public PlanBillingViewModel()
        {
            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                BillingCountry = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                BillingCountry = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                BillingCountry = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                BillingCountry = CountryEnum.BRAZIL;
            }

        }
        public PlanBillingViewModel(string planName, decimal priceToPay)
        {
            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                BillingCountry = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                BillingCountry = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                BillingCountry = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                BillingCountry = CountryEnum.BRAZIL;
            }

            Plan = planName;
            Price = priceToPay.ToString(CultureInfo.CurrentCulture);
        }

        public PlanBillingViewModel(string planName, decimal priceToPay, int id, string planType)
        {
            Plan = planName;
            Price = priceToPay.ToString(CultureInfo.CurrentCulture);
            PlanId = id;
            PlanType = planType;
        }

        public PlanBillingViewModel(Model.Subscription basePlan)
        {
            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                BillingCountry = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                BillingCountry = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                BillingCountry = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                BillingCountry = CountryEnum.BRAZIL;
            }
            PlanType = (basePlan.PlanType != null) ? basePlan.PlanType.Name.ToString() : "";
            Plan = (basePlan.PlanType != null) ? basePlan.Name.Replace("(", "").Replace(")", "").Replace(PlanType, "") : basePlan.Name.Replace("(", "").Replace(")", "");
            Price = basePlan.Amount.ToString();
            PlanId = basePlan.Id;
            BasePlanDescription = basePlan.Description;
            //StartDate = basePlan.StartDate;
            //EndDate = basePlan.RenewalDate ?? DateTime.Today;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BasePlanDescription { get; set; }


        public int PlanId { get; set; }
        public string PlanType { get; set; }
        public string AddPetCharge { get; set; }

        public string Plan { get; set; }
        public string Price { get; set; }
        //[Required(ErrorMessageResourceName = "Profile_PlanEdit_CreditCardTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        //[Display(Name = "Profile_PlanEdit_CreditCardType", ResourceType = typeof(Wording))]
        public CreditCardTypeEnum? CreditCardType { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Profile_PlanEdit_CardRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [CreditCard(ErrorMessageResourceName = "Profile_PlanEdit_CreaditCardValidation", ErrorMessageResourceType = typeof(Wording), ErrorMessage = null)]
        [Required(ErrorMessageResourceName = "Profile_PlanEdit_CreditCardNumberRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_PlanEdit_CreditCardNumber", ResourceType = typeof(Wording))]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessageResourceName = "Profile_PlanEdit_ExpirationDateRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_PlanEdit_ExpirationDate", ResourceType = typeof(Wording))]
        public string ExpirationDate { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Profile_PlanEdit_CVVRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_PlanEdit_CVVRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_PlanEdit_CVV", ResourceType = typeof(Wording))]
        public int? CVV { get; set; }

        #region BillingAddress Fields
        [Display(Name = "Profile_PlanEdit_Address1", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_PlanEdit_Address1Required", ErrorMessageResourceType = typeof(Wording))]
        public string BillingAddress1 { get; set; }

        [Display(Name = "Profile_PlanEdit_Address2", ResourceType = typeof(Wording))]
        public string BillingAddress2 { get; set; }

        [Required(ErrorMessageResourceName = "Profile_PlanEdit_CityRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_PlanEdit_City", ResourceType = typeof(Wording))]
        public string BillingCity { get; set; }

        [Display(Name = "Profile_PlanEdit_Country", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_PlanEdit_CountryRequired", ErrorMessageResourceType = typeof(Wording))]
        public CountryEnum? BillingCountry { get; set; }

        [Display(Name = "Profile_PlanEdit_State", ResourceType = typeof(Wording))]
        public StateEnum? BillingState { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Profile_PlanEdit_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Profile_PlanEdit_ZipRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Profile_PlanEdit_Zip", ResourceType = typeof(Wording))]
        public int? BillingZip { get; set; }
        #endregion
    }
}