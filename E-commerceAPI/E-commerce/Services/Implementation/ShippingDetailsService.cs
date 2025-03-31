using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Repositories;
using E_commerce.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class ShippingDetailsService : IShippingDetailsService
    {
        private readonly IShippingDetailsRepository _shippingDetailsRepository;

        public ShippingDetailsService(IShippingDetailsRepository shippingDetailsRepository)
        {
            _shippingDetailsRepository = shippingDetailsRepository;
        }

        //public async Task<ShippingDetails?> GetShippingDetailsByIdAsync(int id)
        //{
        //    return await _shippingDetailsRepository.GetByIdAsync(id);
        //}
        public async Task<ShippingDetails> GetShippingDetailsByUserIdAsync(int userId)
        {
            // You may also include any validation logic here
            var shippingDetails = await _shippingDetailsRepository.GetByUserIdAsync(userId);

            if (shippingDetails == null)
            {
                throw new KeyNotFoundException($"Shipping details not found for UserId: {userId}");
            }

            return shippingDetails;
        }

        public async Task<IEnumerable<ShippingDetails>> GetAllShippingDetailsAsync()
        {
            return await _shippingDetailsRepository.GetAllAsync();
        }

        public async Task<ShippingDetailsDTO> AddShippingDetailsAsync(int userId, ShippingDetailsCreateDTO shippingDetailsDTO)
        {
            var newShippingDetails = new ShippingDetails
            {
                UserId = userId, // Extracted from JWT token
                Address = shippingDetailsDTO.Address,
                City = shippingDetailsDTO.City,
                PostalCode = shippingDetailsDTO.PostalCode,
                Country = shippingDetailsDTO.Country,
                PhoneNumber = shippingDetailsDTO.PhoneNumber
            };

            await _shippingDetailsRepository.AddShippingDetailsAsync(newShippingDetails);

            return new ShippingDetailsDTO
            {
                ShippingDetailsId = newShippingDetails.ShippingDetailsId,
                Address = newShippingDetails.Address,
                City = newShippingDetails.City,
                PostalCode = newShippingDetails.PostalCode,
                Country = newShippingDetails.Country,
                PhoneNumber = newShippingDetails.PhoneNumber
            };
        }

        public async Task<bool> UpdateShippingDetailsAsync(ShippingDetailsUpdateDTO shippingDetailsDTO)
        {
            var existingDetails = await _shippingDetailsRepository.GetByUserIdAsync(shippingDetailsDTO.ShippingDetailsId);
            if (existingDetails == null) return false;

            existingDetails.Address = shippingDetailsDTO.Address;
            existingDetails.City = shippingDetailsDTO.City;
            existingDetails.PostalCode = shippingDetailsDTO.PostalCode;
            existingDetails.Country = shippingDetailsDTO.Country;
            existingDetails.PhoneNumber = shippingDetailsDTO.PhoneNumber;

            await _shippingDetailsRepository.UpdateShippingDetailsAsync(existingDetails);
            return true;
        }
    


        public async Task<bool> DeleteShippingDetailsAsync(int id)
            {
                var exists = await _shippingDetailsRepository.ExistsAsync(id);
                if (!exists) return false;

                await _shippingDetailsRepository.DeleteAsync(id);
                return true;
            }
        }
}