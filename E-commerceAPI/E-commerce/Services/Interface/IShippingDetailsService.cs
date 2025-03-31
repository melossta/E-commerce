using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public interface IShippingDetailsService
    {
        Task<ShippingDetails> GetShippingDetailsByUserIdAsync(int userId);
        //Task<ShippingDetails?> GetShippingDetailsByIdAsync(int id);
        Task<IEnumerable<ShippingDetails>> GetAllShippingDetailsAsync();
        Task<ShippingDetailsDTO> AddShippingDetailsAsync(int userId, ShippingDetailsCreateDTO shippingDetailsDTO);
        Task<bool> UpdateShippingDetailsAsync(ShippingDetailsUpdateDTO shippingDetailsDTO);
        Task<bool> DeleteShippingDetailsAsync(int id);
    }
}
