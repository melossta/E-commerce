namespace E_commerce.Models.DTOs
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
    public class CategorySummaryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
