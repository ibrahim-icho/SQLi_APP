namespace Sqli_CRM_Web_Application.Responses
{
    public class AuthResponse
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}

