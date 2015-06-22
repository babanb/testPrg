﻿using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.PlansAndPromo
{
    public class AddViewModel
    {
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
        public decimal Price { get; set; }

        [Display(Name = "PlansAndPromo_Add_AdditionalPrice", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_AdditionalPriceRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_AdditionalPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public decimal AdditionalPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_SMOPrice", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_SMOPriceRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_SMOPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public decimal SMOPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_ECPrice", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "PlansAndPromo_Add_ECPriceRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_ECPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public decimal ECPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_MRAPrice", ResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_MRAPriceValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public decimal? MRAPrice { get; set; }

        [Display(Name = "PlansAndPromo_Add_MaxPet", ResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "PlansAndPromo_Add_MaxPetValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? MaxPet { get; set; }

        [Display(Name = "PlansAndPromo_Add_Trial", ResourceType = typeof(Wording))]
        public bool IsTrial { get; set; }

        [Display(Name = "PlansAndPromo_Add_Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "PlansAndPromo_Add_PaymentType", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "IsTrial", TargetValue = false, ErrorMessageResourceName = "PlansAndPromo_Add_PaymentTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        public Model.PaymentTypeEnum? PaymentType { get; set; }


        public Model.Subscription Map()
        {
            var sub = new Model.Subscription
            {
                PaymentTypeId = PaymentType,
                AditionalInfo = AdditionalInfo,
                AmmountPerAddionalPet = AdditionalPrice,
                Amount = Price,
                Description = Description,
                EConsultationAdditionalPrice = ECPrice,
                SMOAdditionalPrice = SMOPrice,
                MRAAdditionalPrice = MRAPrice,
                IsTrial = IsTrial,
                MaxPetCount = Convert.ToInt32(MaxPet),
                Name = PlanName,
                Duration = Convert.ToInt32(TrialDuration),
                LanguageId = Model.LanguageEnum.English,
                PromotionCode = null,
                IsPromotionCode = false,
                IsVisibleToOwner = false,
                Note = Note
            };
            return sub;
        }
    }
}