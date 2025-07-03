using E_commerce.Data;
using E_commerce.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Product>> GetAllProductsAsync()
        //{
        //    return await _context.Products.ToListAsync();
        //}
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductImages) // <-- include related images
                .ToListAsync();
        }


        //public async Task<Product> GetProductByIdAsync(int productId)
        //{
        //    return await _context.Products.FindAsync(productId);
        //}
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.Include(p => p.ProductImages)
                                          .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateProductAsync(Product product)
        //{
        //    _context.Products.Update(product);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task UpdateProductAsync(Product updatedProduct)
        //{
        //    var existingProduct = await _context.Products
        //        .Include(p => p.ProductImages)
        //        .FirstOrDefaultAsync(p => p.ProductId == updatedProduct.ProductId);

        //    if (existingProduct == null) return;

        //    // Update base fields
        //    existingProduct.Name = updatedProduct.Name;
        //    existingProduct.Description = updatedProduct.Description;
        //    existingProduct.Price = updatedProduct.Price;
        //    existingProduct.StockQuantity = updatedProduct.StockQuantity;
        //    existingProduct.CategoryId = updatedProduct.CategoryId;

        //    // Remove all existing images
        //    //_context.ProductImages.RemoveRange(existingProduct.ProductImages);

        //    // Add new images (ignore any IDs)
        //    existingProduct.ProductImages = updatedProduct.ProductImages.Select(img => new ProductImage
        //    {
        //        ImageUrl = img.ImageUrl,
        //        SortOrder = img.SortOrder,
        //        IsPrimary = img.IsPrimary,
        //        ProductId = updatedProduct.ProductId
        //    }).ToList();

        //    await _context.SaveChangesAsync();
        //}
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
