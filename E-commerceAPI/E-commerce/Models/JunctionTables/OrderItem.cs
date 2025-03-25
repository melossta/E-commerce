using E_commerce.Models.Domains;

namespace E_commerce.Models.JunctionTables
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }  // Foreign Key
        public Order Order { get; set; }

        public int ProductId { get; set; }  // Foreign Key
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }

}
