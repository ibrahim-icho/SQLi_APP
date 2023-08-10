namespace Sqli_CRM_Web_Application.Models
{
    public class Incident
    {
        public Guid IncidentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string _customerid_value { get; set; }
        public string _productid_value { get; set; }
        public string ticketnumber { get; set; }
        public DateTime? createdon { get; set; }
        public bool? msdyn_copilotengaged { get; set; }
        public string? name { set; get; } 
        public decimal? price { set; get; } 
        public decimal? currentPrice { set; get; } 
 
    }
}
