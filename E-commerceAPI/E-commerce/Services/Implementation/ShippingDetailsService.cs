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


        //public async Task<ShippingDetailsDto> GetShippingDetailsByUserIdAsync(int userId)
        //{
        //    var shippingDetails = await _shippingDetailsRepository.GetByUserIdAsync(userId);

        //    if (shippingDetails == null)
        //    {
        //        throw new KeyNotFoundException($"Shipping details not found for UserId: {userId}");
        //    }

        //    return new ShippingDetailsDto
        //    {
        //        ShippingDetailsId = shippingDetails.ShippingDetailsId,
        //        UserId = shippingDetails.UserId,
        //        UserName = shippingDetails.User?.Name,
        //        Address = shippingDetails.Address,
        //        City = shippingDetails.City,
        //        PostalCode = shippingDetails.PostalCode,
        //        Country = shippingDetails.Country,
        //        PhoneNumber = shippingDetails.PhoneNumber
        //    };
        //}
        public async Task<IEnumerable<ShippingDetailsDto>> GetShippingDetailsByUserIdAsync(int userId)
        {
            var shippingDetailsList = await _shippingDetailsRepository.GetByUserIdAsync(userId);

            if (shippingDetailsList == null || !shippingDetailsList.Any())
            {
                throw new KeyNotFoundException($"No shipping details found for UserId: {userId}");
            }

            return shippingDetailsList.Select(s => new ShippingDetailsDto
            {
                ShippingDetailsId = s.ShippingDetailsId,
                UserId = s.UserId,
                UserName = s.User?.Name,
                Address = s.Address,
                City = s.City,
                PostalCode = s.PostalCode,
                Country = s.Country,
                PhoneNumber = s.PhoneNumber
            }).ToList();
        }



        public async Task<ShippingDetailsDto> GetByShippingDetailsIdAsync(int id)
        {
            var shippingDetails = await _shippingDetailsRepository.GetByShippingDetailsIdAsync(id);

            if (shippingDetails == null)
                return null;

            return new ShippingDetailsDto
            {
                ShippingDetailsId = shippingDetails.ShippingDetailsId,
                UserId = shippingDetails.UserId,
                UserName = shippingDetails.User?.Name,  // safe navigation
                Address = shippingDetails.Address,
                City = shippingDetails.City,
                PostalCode = shippingDetails.PostalCode,
                Country = shippingDetails.Country,
                PhoneNumber = shippingDetails.PhoneNumber
            };
        }


        public async Task<IEnumerable<ShippingDetailsDto>> GetAllShippingDetailsAsync()
        {
            var shippingDetails = await _shippingDetailsRepository.GetAllAsync();

            // Map entities to DTOs
            return shippingDetails.Select(s => new ShippingDetailsDto
            {
                ShippingDetailsId = s.ShippingDetailsId,
                UserId = s.UserId,
                UserName = s.User?.Name, // safe navigation
                Address = s.Address,
                City = s.City,
                PostalCode = s.PostalCode,
                Country = s.Country,
                PhoneNumber = s.PhoneNumber
            });
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

        public async Task<bool> UpdateShippingDetailsAsync(int id, ShippingDetailsUpdateDTO shippingDetailsDTO)
        {
            var existingDetails = await _shippingDetailsRepository.GetByShippingDetailsIdAsync(id);
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