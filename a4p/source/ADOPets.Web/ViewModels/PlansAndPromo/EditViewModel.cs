﻿using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.PlansAndPromo
{
    public class EditViewModel
    {
        public EditViewModel()
        { }

        public EditViewModel(Model.Subscription subscription)
        {
            Id = subscription.Id;
            PaymentType = subscription.PaymentTypeId;
            AdditionalInfo = subscription.AditionalInfo;
            AdditionalPrice = Convert.ToDecimal(subscription.AmmountPerAddionalPet);
            Price = Convert.ToDecimal(subscription.Amount);
            Description = subscription.Description;
            ECPrice = Convert.ToDecimal(subscription.EConsultationAdditionalPrice);
            SMOPrice = Convert.ToDecimal(subscription.SMOAdditionalPrice);
            if (subscription.MRAAdditionalPrice != 0) { MRAPrice = subscription.MRAAdditionalPrice; };
            IsTrial = Convert.ToBoolean(subscription.IsTrial);
            if (subscription.MaxPetCount != 0) { MaxPet = subscription.MaxPetCount; };
            PlanName = subscription.Name;
            TrialDuration = subscription.Duration;
            Note = subscription.Note;
        }

        public int Id { get; set; }

        [Display(Name = "PlansAndPromo_Add_TrialDuration", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsTrial", TargetValue = true, ErrorMessageResourceName = "PlansAndPromo_Add_TrialDurationRequired", ErrorMessageResourceType = typeof(Wording))]
        public int? TrialDuration { get; set; }

        [Display(Name = "PlansAndPromo_Add_PlanName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_PlanNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        [Display(Name = "PlansAndPromo_Add_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "PlansAndPromo_Add_AdditionalInfo", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_AdditionalInfoRequired", ErrorMessageResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        [Display(Name = "PlansAndPromo_Add_Price", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_PriceRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_PriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        //    [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public decimal Price { get; set; }

        [Display(Name = "PlansAndPromo_Add_AdditionalPrice", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_AdditionalPriceRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_AdditionalPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        // [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public decimal AdditionalPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_SMOPrice", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_SMOPriceRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_SMOPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        //  [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public decimal SMOPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_ECPrice", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_ECPriceRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_ECPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        //  [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public decimal ECPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_MRAPrice", ResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_MRAPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        //[Range(typeof(int), "0", "999", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public decimal? MRAPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_MaxPet", ResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_MaxPetValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        //[Range(typeof(int), "0", "10", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public int? MaxPet { get; set; }

        [Display(Name = "PlansAndPromo_Add_Trial", ResourceType = typeof(Wording))]
        public bool IsTrial { get; set; }

        [Display(Name = "PlansAndPromo_Add_Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "PlansAndPromo_Add_PaymentType", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsTrial", TargetValue = false, ErrorMessageResourceName = "PlansAndPromo_Add_PaymentTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        public Model.PaymentTypeEnum? PaymentType { get; set; }

        public Model.Subscription Map(Model.Subscription Sub)
        {
            Sub.PaymentTypeId = PaymentType;
            Sub.AditionalInfo = AdditionalInfo;
            Sub.AmmountPerAddionalPet = AdditionalPrice;
            Sub.Amount = Price;
            Sub.Description = Description;
            Sub.EConsultationAdditionalPrice = ECPrice;
            Sub.SMOAdditionalPrice = SMOPrice;
            Sub.MRAAdditionalPrice = MRAPrice;
            Sub.IsTrial = IsTrial;
            Sub.MaxPetCount = Convert.ToInt32(MaxPet);
            Sub.Name = PlanName;
            Sub.Duration = Convert.ToInt32(TrialDuration);
            Sub.LanguageId = Model.LanguageEnum.English;
            Sub.Note = Note;
            return Sub;
        }
    }
}