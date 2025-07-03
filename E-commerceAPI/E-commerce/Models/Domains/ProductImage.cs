using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_commerce.Models.Domains
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int ProductId { get; set; }  // Foreign key to Product

        [ForeignKey("ProductId")]
        //[JsonIgnore]
        public Product Product { get; set; }

        // Optional: if you want to order images or mark primary image
        public int SortOrder { get; set; } = 0;
        public bool IsPrimary { get; set; } = false;
    }
}
