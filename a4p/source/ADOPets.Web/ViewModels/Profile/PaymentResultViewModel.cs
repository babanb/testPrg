using System;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.Resources;
using System.ComponentModel.DataAnnotations;
namespace ADOPets.Web.ViewModels.Profile
{
    public class PaymentResultViewModel
    {
        public PaymentResultViewModel()
        {

        }

        public PaymentResultViewModel(PaymentResult result, string plan, DateTime userDateTime, string ActionMade)
        {
            OrderNumber = result.OrderId;
            TransactionID = result.TransactionID;
            TransactionTime = result.TransactionTime;
            TransactionDate = userDateTime.ToShortDateString();
            TransactionResult = result.TransactionResult;
            SubscriptionPlan = plan;
            ErrorMsg = result.ErrorMessage;
            Success = result.Success;
            Action = ActionMade;
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

        public string Action { get; set; }

        [Display(Name = "Account_SignUp_Plan", ResourceType = typeof(Wording))]
        public string SubscriptionPlan { get; set; }
    }
}