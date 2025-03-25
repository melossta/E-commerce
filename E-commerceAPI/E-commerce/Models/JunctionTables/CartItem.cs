using E_commerce.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models.JunctionTables
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int ShoppingCartId { get; set; }  // Foreign Key
        public ShoppingCart ShoppingCart { get; set; }

        public int ProductId { get; set; }  // Foreign Key
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal TotalPrice => Quantity * Product.Price;
    }

}
