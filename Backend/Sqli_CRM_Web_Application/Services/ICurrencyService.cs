using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public interface ICurrencyService
    {
        public Task<TransactionCurrency> GetCurrency(Guid transactioncurrencyid);
    }
}
