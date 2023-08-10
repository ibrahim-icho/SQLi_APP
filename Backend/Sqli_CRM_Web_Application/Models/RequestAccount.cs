namespace Sqli_CRM_Web_Application.Models
{
    public class RequestAccount
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public double? revenue { get; set; } = 0.0;
        public int? statecode { get; set; } = 1;

        public int? opendeals { get; set; } = 0;
    }
}
