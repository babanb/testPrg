using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
namespace ADOPets.Web.ViewModels.Account
{
    public class BillingViewModel
    {
        public BillingViewModel()
        {

        }

        public BillingViewModel(BasicInfoViewModel userInfo, bool flag)
        {
            IsBillingAddressSame = true;
            Price = userInfo.Price.ToString();
            PlanType = userInfo.PlanType;
            PlanName = userInfo.PlanName;
            BasePlanDescription = userInfo.BasePlanDescription;
            UpdateUserInfo(userInfo);
        }


        public BillingViewModel(BasicInfoViewModel userInfo)
        {
            IsBillingAddressSame = true;
            Price = userInfo.Price.ToString();
            UpdateUserInfo(userInfo);
        }

        public void UpdateUserInfo(BasicInfoViewModel userInfo)
        {
            BillingCountry = userInfo.Country;
            Plan = userInfo.PlanName;
            UserAddress1 = userInfo.Address1;
            UserAddress2 = userInfo.Address2;
            UserCity = userInfo.City;
            UserZip = userInfo.Zip;
            UserCountry = userInfo.Country;
            UserState = userInfo.State;
        }

        public string PlanType { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string BasePlanDescription { get; set; }

        public string Plan { get; set; }

        public string Price { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_CreditCardTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_CreditCardType", ResourceType = typeof(Wording))]
        public CreditCardTypeEnum? CreditCardType { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Account_SignUp_CardRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [CreditCard(ErrorMessageResourceName = "Account_SignUp_CreaditCardValidation", ErrorMessageResourceType = typeof(Wording), ErrorMessage = null)]
        [Required(ErrorMessageResourceName = "Account_SignUp_CreditCardNumberRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_CreditCardNumber", ResourceType = typeof(Wording))]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_ExpirationDateRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_ExpirationDate", ResourceType = typeof(Wording))]
        public string ExpirationDate { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Account_SignUp_CVVRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Account_SignUp_CVVRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_CVV", ResourceType = typeof(Wording))]
        public int? CVV { get; set; }

        [Display(Name = "Account_SignUp_SameAddress", ResourceType = typeof(Wording))]
        public bool IsBillingAddressSame { get; set; }

        #region BillingAddress Fields
        [Display(Name = "Account_SignUp_Address1", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Account_SignUp_Address1Required", ErrorMessageResourceType = typeof(Wording))]
        public string BillingAddress1 { get; set; }

        [Display(Name = "Account_SignUp_Address2", ResourceType = typeof(Wording))]
        public string BillingAddress2 { get; set; }

        [Required(ErrorMessageResourceName = "Account_SignUp_CityRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_City", ResourceType = typeof(Wording))]
        public string BillingCity { get; set; }

        [Display(Name = "Account_SignUp_Country", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Account_SignUp_CountryRequired", ErrorMessageResourceType = typeof(Wording))]
        public CountryEnum? BillingCountry { get; set; }

        [Display(Name = "Account_SignUp_State", ResourceType = typeof(Wording))]
        public StateEnum? BillingState { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Account_SignUp_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Account_SignUp_ZipRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Account_SignUp_Zip", ResourceType = typeof(Wording))]
        public int? BillingZip { get; set; }

        #endregion

        #region Basic Info Address

        public string UserAddress1 { get; set; }
        public string UserAddress2 { get; set; }
        public string UserCity { get; set; }
        public int? UserZip { get; set; }
        public StateEnum? UserState { get; set; }
        public CountryEnum? UserCountry { get; set; }

        #endregion
    }
}