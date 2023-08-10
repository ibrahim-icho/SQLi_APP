using Microsoft.AspNetCore.Mvc;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class OpportunityProductsController : ControllerBase
        {

            private readonly IOpportunityProductsService _opportunityproductsService;


            public OpportunityProductsController(IOpportunityProductsService opportunityproductsService)
            {
                _opportunityproductsService = opportunityproductsService;
            }


            [HttpGet]
            public async Task<IActionResult> GetAllOppProducts()
            {
                try
                {
                    List<OpportunityProduct> result = await _opportunityproductsService.GetAllOppProducts();
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Error retrieving opportunities: " + ex.Message);
                }
            }


            // GET api/opportunities/{accountid}
            [HttpGet("product/{productid}")]
            public async Task<ActionResult<List<OpportunityProduct>>> GetOppProductsByProductId(Guid productid)
            {
                try
                {
                    List<OpportunityProduct> oppProducts = await _opportunityproductsService.GetOppProductByProductId(productid);

                    if (oppProducts== null || oppProducts.Count == 0)
                    {
                        return NotFound(); // Return 404 Not Found if no opportunities are found for the given account ID.
                    }

                    return Ok(oppProducts);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that might occur during the retrieval process.
                    // Log the error and return an appropriate response.
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }

            [HttpGet("opportunity/{opportunityid}")]
            public async Task<ActionResult<List<OpportunityProduct>>> GetOppProductsByOppId(Guid opportunityid)
            {
                try
                {
                    List<OpportunityProduct> oppProducts = await _opportunityproductsService.GetOppProductByOppId(opportunityid);

                    if (oppProducts == null || oppProducts.Count == 0)
                    {
                        return NotFound(); // Return 404 Not Found if no opportunities are found for the given account ID.
                    }

                    return Ok(oppProducts);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that might occur during the retrieval process.
                    // Log the error and return an appropriate response.
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }

        [HttpGet("GetOpportunityProductByAccountId/{accountId}")]
        public async Task<ActionResult<List<OpportunityProduct>>> GetOpportunityProductByAccountId(Guid accountId)
        {
            try
            {
                List<OpportunityProduct> oppProducts = await _opportunityproductsService.GetOpportunityProductsByAccountId(accountId);

                if (oppProducts == null || oppProducts.Count == 0)
                {
                    return NotFound(); 
                }

                return Ok(oppProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



    }
}
