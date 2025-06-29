using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models.Domains
{
    public class ShippingDetails
    {
        public int ShippingDetailsId { get; set; }

        public int UserId { get; set; } // Foreign Key
        public User User { get; set; }

        [Required]
        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        // Navigation Property to Orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
