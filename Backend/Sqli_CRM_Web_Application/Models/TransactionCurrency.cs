namespace Sqli_CRM_Web_Application.Models
{
    public class TransactionCurrency
    {
            public string CurrencyName { get; set; }
            public string CurrencySymbol { get; set; }
            public int StateCode { get; set; }
            public int StatusCode { get; set; }
            public DateTime CreatedOn { get; set; }
            public Guid TransactionCurrencyId { get; set; }

     
    }
}
