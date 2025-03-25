using E_commerce.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models.DTOs
{
    public class ProductDTO
    {

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        //public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }  // Foreign Key
    }
}
