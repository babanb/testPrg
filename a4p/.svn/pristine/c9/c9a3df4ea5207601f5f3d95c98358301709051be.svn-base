using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.SMO
{
    public class AddBillingViewModel
    {
        public AddBillingViewModel()
        {
            //if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            //{
            //Country = CountryEnum.UnitedStates;
            //}
            //else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            //{
            //    Country = CountryEnum.France;
            //}
        }

        public long Id { get; set; }

        #region BillingStep

        [Required(ErrorMessageResourceName = "Smo_Add_CreditCardTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_CreditCardType", ResourceType = typeof(Wording))]
        public CreditCardTypeEnum? CreditCardType { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Smo_Add_CardRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [CreditCard(ErrorMessageResourceName = "Smo_Add_CreaditCardValidation", ErrorMessageResourceType = typeof(Wording), ErrorMessage = null)]
        [Required(ErrorMessageResourceName = "Smo_Add_CreditCardNumberRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_CreditCardNumber", ResourceType = typeof(Wording))]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_ExpirationDateRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_ExpirationDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ExpirationDate { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Smo_Add_CCVRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Smo_Add_CCVRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_CCV", ResourceType = typeof(Wording))]
        public string CCV { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_Address1Required", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Smo_Add_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_CityRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_CountryRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_Country", ResourceType = typeof(Wording))]
        public CountryEnum Country { get; set; }

        [Display(Name = "Smo_Add_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Smo_Add_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Smo_Add_ZipRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        public Model.PaymentTypeEnum PaymentTypeId { get; set; }

        public Nullable<int> UserId { get; set; }

        public decimal Price { get; set; }

        public string Plan { get; set; }

        #endregion

        public Model.BillingInformation Map()
        {
            var billinginfo = new Model.BillingInformation
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                CountryId = Country,
                CreditCardTypeId = CreditCardType,
                StateId = State,
                Zip = Zip,
                PaymentTypeId = PaymentTypeId,
                UserId = UserId
            };
            return billinginfo;

        }

        public PaymentHistory MapPayment(Common.Payment.Model.PaymentResult paymentResult, BillingInformation billinginfo)
        {

            var paymentHistory = new PaymentHistory
            {
                Amount = Price,
                PaymentDate = DateTime.Today,
                TransactionNumber = paymentResult.OrderId,
                ErrorMessage = paymentResult.ErrorMessage,
                BillingInformation = billinginfo
            };

            return paymentHistory;
        }
    }
}