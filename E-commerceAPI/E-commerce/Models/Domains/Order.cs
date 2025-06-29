using E_commerce.Models.Enums;
using E_commerce.Models.JunctionTables;

namespace E_commerce.Models.Domains
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; } // Foreign Key
        public User User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }  // Enum: Pending, Shipped, Delivered, Canceled

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public int ShippingDetailsId { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
    }

}
