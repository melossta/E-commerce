using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{userId}")]
        public async Task<ActionResult<ShippingDetails>> GetShippingDetailsByUserId(int userId)
        {
            try
            {
                var shippingDetails = await _shippingDetailsService.GetShippingDetailsByUserIdAsync(userId);
                return Ok(shippingDetails);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);  // Return 404 if the user is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetShippingDetails(int id)
        //{
        //    var shippingDetails = await _shippingDetailsService.GetShippingDetailsByIdAsync(id);
        //    if (shippingDetails == null) return NotFound("Shipping details not found.");
        //    return Ok(shippingDetails);
        //}

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
            return CreatedAtAction(nameof(GetShippingDetailsByUserId), new { id = newShippingDetails.ShippingDetailsId }, newShippingDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShippingDetails(int id, [FromBody] ShippingDetailsUpdateDTO shippingDetailsDTO)
        {
            if (id != shippingDetailsDTO.ShippingDetailsId) return BadRequest("ID mismatch.");
            var updated = await _shippingDetailsService.UpdateShippingDetailsAsync(shippingDetailsDTO);
            if (!updated) return NotFound("Shipping details not found.");
            return NoContent();
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
