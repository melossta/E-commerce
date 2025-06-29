using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models.Domains
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }  // Foreign Key
        public Category Category { get; set; }
    }

}
