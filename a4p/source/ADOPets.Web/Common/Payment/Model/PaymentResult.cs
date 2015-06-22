namespace ADOPets.Web.Common.Payment.Model
{
    public class PaymentResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public string OrderId { get; set; }

        public string TransactionResult { get; set; }
        
        public string TransactionScore { get; set; }

        public string TransactionTime { get; set; }

        public string TransactionID { get; set; }

        public string TransactionDate { get; set; }

        public string CalculatedTax { get; set; }

        public string CalculatedShipping { get; set; }
    }
}