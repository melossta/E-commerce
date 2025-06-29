using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public interface IShippingDetailsService
    {
        Task<IEnumerable<ShippingDetailsDto>> GetShippingDetailsByUserIdAsync(int userId);
        //Task<ShippingDetailsDto> GetShippingDetailsByUserIdAsync(int userId);
        Task<ShippingDetailsDto> GetByShippingDetailsIdAsync(int id);
        Task<IEnumerable<ShippingDetailsDto>> GetAllShippingDetailsAsync();
        Task<ShippingDetailsDTO> AddShippingDetailsAsync(int userId, ShippingDetailsCreateDTO shippingDetailsDTO);
        Task<bool> UpdateShippingDetailsAsync(int id, ShippingDetailsUpdateDTO shippingDetailsDTO);
        Task<bool> DeleteShippingDetailsAsync(int id);

    }
}
