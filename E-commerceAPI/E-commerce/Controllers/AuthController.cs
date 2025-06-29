using Microsoft.AspNetCore.Mvc;
using E_commerce.Models.Requests;
using E_commerce.Models.Responses;
using E_commerce.Services.Interface;
using E_commerce.Models.Responses;
using E_commerce.Services.Interface;

namespace HippAdministrata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var success = await _jwtService.RegisterAsync(request);

            if (!success)
            {
                return BadRequest(new { message = "Registration failed. The user may already exist." });
            }

            return Ok(new { message = "User successfully registered" });
        }


        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest request)
        {
            var response = await _jwtService.AuthenticateAsync(request);

            if (response == null)
                return Unauthorized("Invalid credentials");

            return Ok(response);  // Return the token and role if successful
        }
    }
}
