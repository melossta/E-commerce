using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var productDTOs = await _productService.GetAllProductsAsync();
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productDTO = await _productService.GetProductByIdAsync(id);
            if (productDTO == null)
                return NotFound();

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product name is required");

            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ProductImages = product.ProductImages.Select(img => new ProductImage
                {
                    //ProductImageId = img.ProductImageId,
                    ImageUrl = img.ImageUrl,
                    SortOrder = img.SortOrder,
                    IsPrimary = img.IsPrimary
                }).ToList()

            };


            await _productService.AddProductAsync(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);

        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDto updateDto)
        {
            var existingProductDTO = await _productService.GetProductByIdAsync(productId);
            if (existingProductDTO == null) return NotFound();

            // Create a real Product domain model to pass to UpdateProductAsync
            var updatedProduct = new Product
            {
                //ProductId = productId,
                Name = updateDto.Name,
                Description = updateDto.Description,
                Price = updateDto.Price,
                StockQuantity = updateDto.StockQuantity,
                CategoryId = updateDto.CategoryId,
                ProductImages = updateDto.ProductImages.Select(img => new ProductImage
                {
                    //ProductImageId = img.ProductImageId,
                    ImageUrl = img.ImageUrl,
                    SortOrder = img.SortOrder,
                    IsPrimary = img.IsPrimary
                }).ToList()
            };

            await _productService.UpdateProductAsync(productId, updateDto);
            return NoContent();
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
