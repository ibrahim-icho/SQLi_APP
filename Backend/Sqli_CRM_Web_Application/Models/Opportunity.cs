using System.Text;

namespace Sqli_CRM_Web_Application.Models
{
    public class Opportunity
    {
        public DateTime estimatedclosedate { get; set; }
        public Guid opportunityid { get; set; }
        public Guid _parentcontactid_value { get; set; }
        public Guid _parentaccountid_value { get; set; }
        public decimal totallineitemamount { get; set; }
        public string? name { get; set; }
        public string? emailaddress { get; set; }
        public decimal? estimatedvalue { get; set; }
        public string? description { get; set; }
        public string? currentsituation { get; set; }
        public int salesstagecode { get; set; }
        public decimal? totallineitemdiscountamount_base { get; set; }
        public int? purchasetimeframe { get; set; }
        public int? closeprobability { get; set; }
        public Guid _customerid_value { get; set; }
        public Guid _transactioncurrencyid_value { get; set; }
        public string? currency { get; set; }
        public  string? CurrencySymbol { get; set; } 
    }

}
