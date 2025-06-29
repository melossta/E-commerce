using E_commerce.Models.Enums;

namespace E_commerce.Models.Domains
{
    public class PaymentDetails
    {
        public int PaymentDetailsId { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public PaymentMethod PaymentMethod { get; set; }  // Enum: CreditCard, PayPal, BankTransfer

        // Safe to store:
        public string LastFourDigits { get; set; } // Only the last 4 digits for reference
        public string BillingAddress { get; set; }
        public string TransactionId { get; set; } // From the payment gateway
    }

}
