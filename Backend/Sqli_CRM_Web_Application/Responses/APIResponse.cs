using System.Net;

namespace Sqli_CRM_Web_Application.Responses
{
    public class APIResponse
    {
        public HttpStatusCode httpStatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = null;
        public object? Result { get; set; } = null;
    }
}
