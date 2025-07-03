using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;

namespace E_commerce.Services
{
    public interface IProductService
    {
        //Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        //Task<Product> GetProductByIdAsync(int productId);
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        //Task UpdateProductAsync(Product product);
        Task UpdateProductAsync(int productId, UpdateProductDto dto);
        Task DeleteProductAsync(int productId);
    }
}
