using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sqli_CRM_Web_Application.Connection;
using Sqli_CRM_Web_Application.Context;
using Sqli_CRM_Web_Application.Helpers;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Requests;
using Sqli_CRM_Web_Application.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sqli_CRM_Web_Application.Services
{
    public class AuthService : IAuthService
    {


        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly Dynamics365Connection _crmConnection;
        private readonly string baseUrl;
        private readonly IConfiguration _configuration;


        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt,
        Dynamics365Connection crmConnection, IConfiguration _configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _crmConnection = crmConnection;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";

        }

        // this function to check that the email exists in CRM 
        private async Task<bool> CheckEmailExistsInCRMAsync(string email)
        {

            string accessToken = await _crmConnection.GetAccessTokenAsync();

            string crmQuery = $"{baseUrl}/contacts?$filter=emailaddress1 eq '{email}'";

            HttpResponseMessage response = await _crmConnection.CrmRequest(HttpMethod.Get, accessToken, crmQuery);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic contactData = JsonConvert.DeserializeObject(responseBody);
                return contactData != null && contactData.value.Count > 0;

            }

            return false;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // this for checking that the email exists in CRM 
            bool emailExistsInCRM = await CheckEmailExistsInCRMAsync(request.Email);

            if (!emailExistsInCRM)
            {
                return new AuthResponse
                {
                    Message = "Email is Not registered in CRM!",
                    IsAuthenticated = false,
                };
            }
           
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponse
                {
                    Message = "Email is already registered in the application!",
                    IsAuthenticated = false
                };
            }

            
            User user = new()
            {
                UserName = request.Username,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname
            };

      
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                string errors = String.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}\n";
                }
                return new AuthResponse
                {
                    Message = errors,
                };
            }

            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthResponse
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var authResponse = new AuthResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                authResponse.Message = "Email or password is incorrect !";
                return authResponse;
            }


            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authResponse.IsAuthenticated = true;
            authResponse.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return authResponse;
        }

        public async Task<string> AddRoleAsync(AddRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(request.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, request.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, request.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
                Items =
            {
                { "LoginProvider", provider },
                { "XsrfKey", "1" },
            },
            };
        }

    }
}