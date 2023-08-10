using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using Sqli_CRM_Web_Application.Connection;
using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public class OpportunityProductsService:IOpportunityProductsService
    {
        private readonly Dynamics365Connection _connection;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;
        private readonly IOpportunitiesService _opportunitiesService;

  

        public OpportunityProductsService(Dynamics365Connection connection, IConfiguration configuration,IOpportunitiesService opportunitiesService)
        {
            _opportunitiesService = opportunitiesService;
            _configuration = configuration;
            _connection = connection;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }



        // Retrive all opportunities
        async Task<List<OpportunityProduct>> IOpportunityProductsService.GetAllOppProducts()
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/opportunityproducts");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve opportunities. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<OpportunityProduct>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<OpportunityProduct>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunities: {ex.Message}");
                return new List<OpportunityProduct>();
            }
        }
        async Task<List<OpportunityProduct>> IOpportunityProductsService.GetOppProductByProductId(Guid productid)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/opportunityproducts?$filter=_productid_value eq '{productid}'");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve opportunity. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<OpportunityProduct>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<OpportunityProduct>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunities: {ex.Message}");
                return new List<OpportunityProduct>();
            }
        }

        async Task<List<OpportunityProduct>> IOpportunityProductsService.GetOppProductByOppId(Guid opportunityid)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/opportunityproducts?$filter=_opportunityid_value eq {opportunityid}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve opportunity. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<OpportunityProduct>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<OpportunityProduct>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunities: {ex.Message}");
                return new List<OpportunityProduct>();
            }
        }



        public async Task<List<OpportunityProduct>> GetOpportunityProductsByAccountId(Guid accountId)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var opportunities = await _opportunitiesService.GetOpportunitiesByAccountId(accountId);

                List<OpportunityProduct> allOpportunityProducts = new List<OpportunityProduct>();

                foreach (var opportunity in opportunities)
                {
                    var response = await _connection.CrmRequest(
                        HttpMethod.Get,
                        accessToken,
                        $"{baseUrl}/opportunityproducts?$filter=_opportunityid_value eq {opportunity.opportunityid}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(json);
                        var opportunityProducts = result?.value?.ToObject<List<OpportunityProduct>>();

                        if (opportunityProducts != null)
                        {
                            allOpportunityProducts.AddRange(opportunityProducts);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve opportunity products. Status code: {response.StatusCode}");
                    }
                }

                return allOpportunityProducts;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunity products: {ex.Message}");
                return new List<OpportunityProduct>();
            }
        }



    }
}
