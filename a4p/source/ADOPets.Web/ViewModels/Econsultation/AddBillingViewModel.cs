﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class AddBillingViewModel
    {
        public AddBillingViewModel()
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
        }


        public AddBillingViewModel(AddBillingViewModel userInfo)
        {
            UpdateUserInfo(userInfo);
        }

        public void UpdateUserInfo(AddBillingViewModel userInfo)
        {
            BillingCountry = userInfo.BillingCountry;
            Address1 = userInfo.Address1;
            Address2 = userInfo.Address2;
            City = userInfo.City;
            Zip = userInfo.Zip;
            BillingState = userInfo.BillingState;
            PetId = userInfo.PetId;
        }


        [Required(ErrorMessageResourceName = "Econsultation_Add_CreditCardTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Econsultation_Add_CreditCardType", ResourceType = typeof(Wording))]
        public CreditCardTypeEnum? CreditCardType { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Econsultation_Add_CardRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [CreditCard(ErrorMessageResourceName = "Econsultation_Add_CreaditCardValidation", ErrorMessageResourceType = typeof(Wording), ErrorMessage = null)]
        [Required(ErrorMessageResourceName = "Econsultation_Add_CreditCardNumberRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Econsultation_Add_CreditCardNumber", ResourceType = typeof(Wording))]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessageResourceName = "Econsultation_Add_ExpirationDateRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Econsultation_Add_ExpirationDate", ResourceType = typeof(Wording))]
        public string ExpirationDate { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Econsultation_Add_CVVRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_CVVRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Econsultation_Add_CVV", ResourceType = typeof(Wording))]
        public int? CVV { get; set; }

        #region BillingAddress Fields
        [Display(Name = "Econsultation_Add_Address1", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_Address1Required", ErrorMessageResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Econsultation_Add_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Required(ErrorMessageResourceName = "Econsultation_Add_CityRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Econsultation_Add_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "Econsultation_Add_CountryRequired", ErrorMessageResourceType = typeof(Wording))]
        public CountryEnum? BillingCountry { get; set; }

        [Required(ErrorMessageResourceName = "Econsultation_Add_StateRequired", ErrorMessageResourceType = typeof(Wording))]
        public StateEnum? BillingState { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Econsultation_Add_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ZipRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Econsultation_Add_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        public Model.PaymentTypeEnum PaymentTypeId { get; set; }

        public Nullable<int> UserId { get; set; }

        public Nullable<int> PetId { get; set; }

        #endregion
        public Model.BillingInformation Map()
        {
            var billinginfo = new Model.BillingInformation
            {
                Address1 = new EncryptedText(Address1),
                Address2 = new EncryptedText(Address2),
                City = new EncryptedText(City),
                CreditCardTypeId = CreditCardType,
                StateId = BillingState,
                CountryId = BillingCountry.Value,
                Zip = new EncryptedText(Zip),
                PaymentTypeId = PaymentTypeId,
                UserId = UserId,                
            };
            return billinginfo;

        }


    }
}
