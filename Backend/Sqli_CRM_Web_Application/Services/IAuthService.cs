using Microsoft.AspNetCore.Authentication;
using Sqli_CRM_Web_Application.Requests;
using Sqli_CRM_Web_Application.Responses;

namespace Sqli_CRM_Web_Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest model);
        Task<AuthResponse> LoginAsync(LoginRequest model);
        public Task<string> AddRoleAsync(AddRoleRequest model);
        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
    }
}
