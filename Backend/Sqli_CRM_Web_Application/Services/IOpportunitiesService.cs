using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public interface IOpportunitiesService
    {

        // Retrive all opportunities
        public Task<List<Opportunity>> GetAllOpportunities();

        // retrive opportunity
        public Task<Opportunity> GetOpportunityByID(Guid opportunityid);

       // retrive opportunities by contact id 
        public Task<List<Opportunity>> GetOpportunitiesByContactId(Guid contactid);

        // retrive opportunities by account id 
        public Task<List<Opportunity>> GetOpportunitiesByAccountId(Guid accountid);

    }
}
