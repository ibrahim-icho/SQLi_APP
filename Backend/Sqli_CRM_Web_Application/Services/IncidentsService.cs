using Newtonsoft.Json;
using Sqli_CRM_Web_Application.Connection;
using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public class IncidentsService:IIncidentsService
    {
        private readonly Dynamics365Connection _connection;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public IncidentsService(Dynamics365Connection connection, IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = connection;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }
        async Task IIncidentsService.CreateIncident(Incident incident)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var incidentDetails = new
                {
                    incidentid = incident.IncidentId,
                    title = incident.Title,
                    description = incident.Description
                };

                string incidentJson = JsonConvert.SerializeObject(incidentDetails);


                var response = await _connection.CrmRequest(
                    HttpMethod.Post,
                    accessToken,
                    $"{baseUrl}/incidents",
                    incidentJson);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve incidents. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Incident>>();

            }
            catch (Exception ex)
            {
                // Log the exception as per your logging strategy
                Console.WriteLine($"An error occurred: {ex.Message}");
            }



        }

        async Task<List<Incident>> IIncidentsService.GetAllIncidents()
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/incidents");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve incidents. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Incident>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<Incident>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                return new List<Incident>();
            }
        }

       async  Task<List<Incident>> IIncidentsService.GetIncidentsByCustomerId(Guid customerId)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/incidents?$filter=_customerid_value eq {customerId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve incidents. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Incident>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<Incident>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                return new List<Incident>();
            }
        }
    }
}
