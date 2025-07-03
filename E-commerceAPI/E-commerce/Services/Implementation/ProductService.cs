using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Repositories;

namespace E_commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //public async Task<IEnumerable<Product>> GetAllProductsAsync()
        //{
        //    return await _productRepository.GetAllProductsAsync();
        //}

        //public async Task<Product> GetProductByIdAsync(int productId)
        //{
        //    return await _productRepository.GetProductByIdAsync(productId);
        //}
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            var productDTOs = products.Select(p => new ProductDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                ProductImages = p.ProductImages?.Select(img => new ProductImageDTO
                {
                    ProductImageId = img.ProductImageId,
                    ImageUrl = img.ImageUrl,
                    SortOrder = img.SortOrder,
                    IsPrimary = img.IsPrimary
                }).ToList() ?? new List<ProductImageDTO>()
            });

            return productDTOs;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) return null;

            var productDTO = new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                ProductImages = product.ProductImages?.Select(img => new ProductImageDTO
                {
                    ProductImageId = img.ProductImageId,
                    ImageUrl = img.ImageUrl,
                    SortOrder = img.SortOrder,
                    IsPrimary = img.IsPrimary
                }).ToList() ?? new List<ProductImageDTO>()
            };

            return productDTO;
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        //public async Task UpdateProductAsync(Product product)
        //{
        //    await _productRepository.UpdateProductAsync(product);
        //}
        public async Task UpdateProductAsync(int productId, UpdateProductDto updateDto)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(productId);
            if (existingProduct == null) return;

            // Update base fields
            existingProduct.Name = updateDto.Name;
            existingProduct.Description = updateDto.Description;
            existingProduct.Price = updateDto.Price;
            existingProduct.StockQuantity = updateDto.StockQuantity;
            existingProduct.CategoryId = updateDto.CategoryId;

            // Re-map images
            existingProduct.ProductImages = updateDto.ProductImages.Select(img => new ProductImage
            {
                ImageUrl = img.ImageUrl,
                SortOrder = img.SortOrder,
                IsPrimary = img.IsPrimary,
                ProductId = productId
            }).ToList();

            await _productRepository.UpdateProductAsync(existingProduct);
        }


        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }
    }
}
