namespace Sqli_CRM_Web_Application.Models
{
    public class Product
    {
        public Guid productid { set; get; }
        public string? name { set; get; }
        public string description { set; get; }
        public decimal? price { set; get; }
        public decimal? currentPrice { set; get; }
        public DateTime createdon { set; get; }
        public string productnumber { set; get; }
        public int producttypecode { set; get; }
        public decimal? stockvolume { set; get; }
    }

}
