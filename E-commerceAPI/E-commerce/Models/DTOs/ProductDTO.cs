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
        public List<ProductImageDTO> ProductImages { get; set; } = new List<ProductImageDTO>();
    }
    public class ProductImageDTO
    {
        public int ProductImageId { get; set; }
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class UpdateProductImageDTO
    {

        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class UpdateProductDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public List<UpdateProductImageDTO> ProductImages { get; set; } = new List<UpdateProductImageDTO>();
    }

    public class AddProductDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public List<UpdateProductImageDTO> ProductImages { get; set; } = new List<UpdateProductImageDTO>();
    }


}
