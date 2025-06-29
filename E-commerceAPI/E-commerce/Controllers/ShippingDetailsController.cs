using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingDetailsController : ControllerBase
    {
        private readonly IShippingDetailsService _shippingDetailsService;

        public ShippingDetailsController(IShippingDetailsService shippingDetailsService)
        {
            _shippingDetailsService = shippingDetailsService;
        }


        //[HttpGet("by-user/{userId}")]
        //public async Task<ActionResult<IEnumerable<ShippingDetailsDto>>> GetShippingDetailsByUserId(int userId)
        //{
        //    try
        //    {
        //        var shippingDetails = await _shippingDetailsService.GetShippingDetailsByUserIdAsync(userId);
        //        return Ok(shippingDetails);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<ShippingDetailsDto>>> GetShippingDetailsByUserId(int userId)
        {
            try
            {
                var shippingDetails = await _shippingDetailsService.GetShippingDetailsByUserIdAsync(userId);
                return Ok(shippingDetails);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingDetailsDto>> GetShippingDetailsById(int id)
        {
            var shippingDetails = await _shippingDetailsService.GetByShippingDetailsIdAsync(id);

            if (shippingDetails == null)
                return NotFound();

            return Ok(shippingDetails);
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAllShippingDetails()
        {
            var shippingDetailsList = await _shippingDetailsService.GetAllShippingDetailsAsync();
            return Ok(shippingDetailsList);
        }

        [HttpPost]
        public async Task<IActionResult> AddShippingDetails([FromBody] ShippingDetailsCreateDTO shippingDetailsDTO)
        {
            if (shippingDetailsDTO == null) return BadRequest("Invalid shipping details data.");

            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (userIdClaim == null) return Unauthorized("User ID missing from token.");
            int userId = int.Parse(userIdClaim);

            var newShippingDetails = await _shippingDetailsService.AddShippingDetailsAsync(userId, shippingDetailsDTO);
            //return CreatedAtAction(nameof(GetShippingDetailsByUserId), new { id = newShippingDetails.ShippingDetailsId }, newShippingDetails);
            return Ok(new { message = "Shipping created successfully." });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShippingDetails(int id, [FromBody] ShippingDetailsUpdateDTO shippingDetailsDTO)
        {
            var updated = await _shippingDetailsService.UpdateShippingDetailsAsync(id, shippingDetailsDTO);
            if (!updated) return NotFound("Shipping details not found.");

            return Ok(new { message = "Shipping updated successfully." });

        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingDetails(int id)
        {
            var deleted = await _shippingDetailsService.DeleteShippingDetailsAsync(id);
            if (!deleted) return NotFound("Shipping details not found.");
            return NoContent();
        }
    }
}
