using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OpportunitiesController : ControllerBase
    {

        private readonly IOpportunitiesService _opportunitiesService;
        private readonly ICurrencyService _currencyService;
       

        public OpportunitiesController(IOpportunitiesService opportunitiesService,ICurrencyService currencyService)
        {
            _opportunitiesService = opportunitiesService;
            _currencyService = currencyService;

        }

      

        [HttpGet]
        public async Task<IActionResult> GetAllOpportunities()
        {
            try
            {
                List<Opportunity> result = await _opportunitiesService.GetAllOpportunities();

                foreach (var opportunity in result)
                {
                    var currencyId = opportunity._transactioncurrencyid_value;
                    var calculatedCurrency = await ((ICurrencyService)_currencyService).GetCurrency(currencyId);

                    var responseOpportunity = opportunity;
                    if (responseOpportunity != null)
                    {
                        opportunity.currency = calculatedCurrency.CurrencyName;
                        opportunity.CurrencySymbol = calculatedCurrency.CurrencySymbol;
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {   
                return StatusCode(500, "Error retrieving opportunities: " + ex.Message);
            }
        }

        [HttpGet("{opportunityid}")]
        public async Task<IActionResult> GetOpportunityById(Guid opportunityid)
        {
            try
            {
                Opportunity opportunity = await _opportunitiesService.GetOpportunityByID(opportunityid);
                var calculatedCurrency = await ((ICurrencyService)_currencyService).GetCurrency(opportunity._transactioncurrencyid_value);
                opportunity.currency = calculatedCurrency.CurrencyName;
                opportunity.CurrencySymbol = calculatedCurrency.CurrencySymbol;
                return Ok(opportunity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        // GET api/opportunities/{accountid}
        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetOpportunitiesByAccountId(Guid accountid)
        {
            try
            {
                List<Opportunity> opportunities = await _opportunitiesService.GetOpportunitiesByAccountId(accountid);

                if (opportunities == null || opportunities.Count == 0)
                {
                    return NotFound(); // Return 404 Not Found if no opportunities are found for the given account ID.
                }

                foreach (var opportunity in opportunities)
                {
                    var currencyId = opportunity._transactioncurrencyid_value;
                    var calculatedCurrency = await ((ICurrencyService)_currencyService).GetCurrency(currencyId);

                    var responseOpportunity = opportunity;
                    if (responseOpportunity != null)
                    {
                        opportunity.currency = calculatedCurrency.CurrencyName;
                        opportunity.CurrencySymbol = calculatedCurrency.CurrencySymbol;
                    }
                }

                return Ok(opportunities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        // GET api/opportunities/{contactid}
        [HttpGet("{parentcontactid}")]
        public async Task<IActionResult> GetOpportunitiesByA(Guid parentcontactid)
        {
            try
            {
                List<Opportunity> opportunities = await _opportunitiesService.GetOpportunitiesByContactId(parentcontactid);

                if (opportunities == null || opportunities.Count == 0)
                {
                    return NotFound(); // Return 404 Not Found if no opportunities are found for the given account ID.
                }


                foreach (var opportunity in opportunities)
                {
                    var currencyId = opportunity._transactioncurrencyid_value;
                    var calculatedCurrency = await ((ICurrencyService)_currencyService).GetCurrency(currencyId);

                    var responseOpportunity = opportunity;

                    if (responseOpportunity != null)
                    {
                        opportunity.currency = calculatedCurrency.CurrencyName;
                        opportunity.CurrencySymbol = calculatedCurrency.CurrencySymbol;
                    }
                }
                return Ok(opportunities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
