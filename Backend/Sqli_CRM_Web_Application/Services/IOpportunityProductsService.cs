using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public interface IOpportunityProductsService
    {
        public Task<List<OpportunityProduct>> GetAllOppProducts();

        public Task<List<OpportunityProduct>> GetOppProductByProductId(Guid productid);

        public Task<List<OpportunityProduct>> GetOppProductByOppId(Guid opportunityid);

        public Task<List<OpportunityProduct>> GetOpportunityProductsByAccountId(Guid accountId);

    }
}
