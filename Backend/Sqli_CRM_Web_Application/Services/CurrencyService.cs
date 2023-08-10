using Newtonsoft.Json;
using Sqli_CRM_Web_Application.Connection;
using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly Dynamics365Connection _connection;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public CurrencyService(Dynamics365Connection connection, IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = connection;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }
       async  Task<TransactionCurrency> ICurrencyService.GetCurrency(Guid transactioncurrencyid)
        {

           
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                $"{baseUrl}/transactioncurrencies({transactioncurrencyid})" 
                );

                var json = await response.Content.ReadAsStringAsync();
                var transactioncurrency = JsonConvert.DeserializeObject<TransactionCurrency>(json);

                return transactioncurrency;
            
        }
    }
}
