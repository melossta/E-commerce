using E_commerce.Models.JunctionTables;

namespace E_commerce.Models.Domains
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        public int UserId { get; set; } // Foreign Key
        public User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }

}
