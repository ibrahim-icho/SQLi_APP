using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public interface IIncidentsService
    {
        public Task CreateIncident(Incident incident);
        public Task<List<Incident>> GetAllIncidents();
        public Task<List<Incident>> GetIncidentsByCustomerId(Guid customerId);
    }
}
