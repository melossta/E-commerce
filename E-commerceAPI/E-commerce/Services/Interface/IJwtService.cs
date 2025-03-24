using E_commerce.Models.Responses;
using E_commerce.Models.Requests;
using E_commerce.Models.DTOs;

namespace E_commerce.Services.Interface
{
    public interface IJwtService
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}
