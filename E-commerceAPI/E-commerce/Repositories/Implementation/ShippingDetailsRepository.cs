using E_commerce.Data;
using E_commerce.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerce.Repositories
{
    public class ShippingDetailsRepository : IShippingDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public ShippingDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<ShippingDetails?> GetByIdAsync(int id)
        //{
        //    return await _context.ShippingDetails.FindAsync(id);
        //}
        public async Task<ShippingDetails> GetByUserIdAsync(int userId)
        {
            return await _context.ShippingDetails
                                 .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<IEnumerable<ShippingDetails>> GetAllAsync()
        {
            return await _context.ShippingDetails.ToListAsync();
        }

        public async Task AddShippingDetailsAsync(ShippingDetails shippingDetails) // FIXED
        {
            await _context.ShippingDetails.AddAsync(shippingDetails);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShippingDetailsAsync(ShippingDetails shippingDetails) // FIXED
        {
            _context.ShippingDetails.Update(shippingDetails);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shippingDetails = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetails != null)
            {
                _context.ShippingDetails.Remove(shippingDetails);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ShippingDetails.AnyAsync(s => s.ShippingDetailsId == id);
        }
    }
}
