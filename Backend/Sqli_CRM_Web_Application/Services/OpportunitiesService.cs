using Newtonsoft.Json;
using Sqli_CRM_Web_Application.Connection;
using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public class OpportunitiesService : IOpportunitiesService
    {

        private readonly Dynamics365Connection _connection;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public OpportunitiesService(Dynamics365Connection connection, IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = connection;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }


        // Retrive all opportunities
        async Task<List<Opportunity>> IOpportunitiesService.GetAllOpportunities()
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/opportunities");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve opportunities. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Opportunity>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<Opportunity>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunities: {ex.Message}");
                return new List<Opportunity>();
            }
        }


        // Retrive opportuniy by id
        async Task<Opportunity> IOpportunitiesService.GetOpportunityByID(Guid opportunityid)
        {
            string accessToken = await _connection.GetAccessTokenAsync();
           
            var response = await _connection.CrmRequest(
                HttpMethod.Get,
                accessToken,
            $"{baseUrl}/opportunities({opportunityid})");

            var json = await response.Content.ReadAsStringAsync();
            var opportunity = JsonConvert.DeserializeObject<Opportunity>(json);

            return opportunity!;
        }


        // Retrive opportunity by account id 
        async Task<List<Opportunity>> IOpportunitiesService.GetOpportunitiesByAccountId(Guid accountid)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/opportunities?$filter=_parentaccountid_value eq {accountid}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve opportunity. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Opportunity>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<Opportunity>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunities: {ex.Message}");
                return new List<Opportunity>();
            }
        }

        // Retrive opportunity by contact id 
         async  Task<List<Opportunity>> IOpportunitiesService.GetOpportunitiesByContactId(Guid contactid)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/opportunities?$filter=_parentcontactid_value eq {contactid}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve opportunity. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Opportunity>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<Opportunity>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunities: {ex.Message}");
                return new List<Opportunity>();
            }
        }





      

    }
}
