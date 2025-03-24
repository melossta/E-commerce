using System.Collections.Generic;


namespace E_commerce.Models.Responses
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
