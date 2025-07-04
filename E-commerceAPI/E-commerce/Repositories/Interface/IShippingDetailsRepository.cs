﻿using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Models.Responses;
using System.Threading.Tasks;

namespace E_commerce.Repositories
{
    public interface IShippingDetailsRepository
    {
        Task<IEnumerable<ShippingDetails>> GetByUserIdAsync(int userId);
        // qeta e kem bno koment
        //Task<ShippingDetails> GetByUserIdAsync(int userId);
        Task<ShippingDetails> GetByShippingDetailsIdAsync(int id);
        Task<IEnumerable<ShippingDetails>> GetAllAsync();
        Task AddShippingDetailsAsync(ShippingDetails shippingDetails); // FIXED: Matches 
        Task UpdateShippingDetailsAsync(ShippingDetails shippingDetails); // FIXED: Matches service
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);



    }
}
