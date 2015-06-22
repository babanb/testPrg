using System;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.Resources;
using System.ComponentModel.DataAnnotations;
namespace ADOPets.Web.ViewModels.Account
{
    public class PaymentResultViewModel
    {
        public PaymentResultViewModel()
        {

        }
        public PaymentResultViewModel(bool isFreeUser)
        {
            Success = true;
        }
        public PaymentResultViewModel(PaymentResult result, string plan, DateTime userDateTime)
        {
            OrderNumber = result.OrderId;
            TransactionID = result.TransactionID;
            TransactionTime = result.TransactionTime;
            TransactionDate = userDateTime.ToShortDateString();
            TransactionResult = result.TransactionResult;
            SubscriptionPlan = plan;
            ErrorMsg = result.ErrorMessage;
            Success = result.Success;
        }

        [Display(Name = "Account_SignUp_OrderNumber", ResourceType = typeof(Wording))]
        public string OrderNumber { get; set; }

        [Display(Name = "Account_SignUp_TransactionID", ResourceType = typeof(Wording))]
        public string TransactionID { get; set; }

        [Display(Name = "Account_SignUp_TransactionDate", ResourceType = typeof(Wording))]
        public string TransactionDate { get; set; }

        [Display(Name = "Account_SignUp_TransactionTime", ResourceType = typeof(Wording))]
        public string TransactionTime { get; set; }

        [Display(Name = "Account_SignUp_TransactionResult", ResourceType = typeof(Wording))]
        public string TransactionResult { get; set; }
                
        public string ErrorMsg { get; set; }

        public bool Success { get; set; }

        [Display(Name = "Account_SignUp_Plan", ResourceType = typeof(Wording))]
        public string SubscriptionPlan { get; set; }

        [Display(Name = "Smo_Add_SMOID", ResourceType = typeof(Wording))]
        public string SMOID { get; set; }

        [Display(Name = "Smo_Add_Price", ResourceType = typeof(Wording))]
        public string Price { get; set; }
    }
}