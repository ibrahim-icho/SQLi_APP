using System.Text.Json.Serialization;

namespace Sqli_CRM_Web_Application.Models
{
    public class OpportunityProduct
    {
        public Guid _productid_value { set; get; }
        public Guid _opportunityid_value { get; set; }
        public string opportunityproductname { set; get; } //productname
        public string productdescription { get; set; }

        public decimal priceperunit { set; get; } //price

        public decimal extendedamount { set; get; } // this represents the total price of the opportunity (priceperunit * quantity).

        public decimal baseamount { set; get; } //represents the base amount (usually price per unit * quantity) 


    }
}
