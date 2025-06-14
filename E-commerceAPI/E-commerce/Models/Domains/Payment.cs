using E_commerce.Models.Enums;

namespace E_commerce.Models.Domains
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; }
        public string TransactionId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }
        public int UserId { get; set; } // 🔹 New Foreign Key
        //public User User { get; set; }  // 🔹 Navigation Property
        /*public string PaymentMethod { get; set; } = "Credit Card";*/ // Enum: Credit Card, PayPal, Bank Transfer
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        /*public string Status { get; set; } = "Processing";*/ // Enum: Processing, Completed, Failed
        public OrderStatus Status { get; set; }

    }

}
