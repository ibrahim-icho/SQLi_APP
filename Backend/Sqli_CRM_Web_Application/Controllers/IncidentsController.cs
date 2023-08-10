using Microsoft.AspNetCore.Mvc;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;
using System;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentsService _incidentService;
        private readonly IProductsService _productsService;
        public IncidentsController(IIncidentsService incidentService,IProductsService productsService)
        {
            _incidentService = incidentService;
            _productsService = productsService;
        }

        /*
        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] Incident incident)
        {
            try
            {
                await _incidentService.CreateIncident(incident);

                return Ok("Incident created successfully.");
            }
            catch (Exception ex)
            {
                // Log error here
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("test-create")]
        public async Task<IActionResult> TestCreateIncident()
        {
            try
            {
                var incident = new Incident
                {
                    IncidentId = Guid.NewGuid(),
                    Title = "Test Title",
                    Description = "Test Description"
                };

                await _incidentService.CreateIncident(incident);
                return Ok("Test Incident created successfully.");
            }
            catch (Exception ex)
            {
                // Log error here
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        */


        [HttpGet]
        public async Task<IActionResult> GetAllIncidents()
        {
            try
            {
                List<Incident> result = await _incidentService.GetAllIncidents();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error retrieving : " + ex.Message);
            }
        }



        [HttpGet("{customerId}")]

        public async Task<IActionResult> GetIncidentesByCustomerId(Guid customerId)
        {
            try
            {
                List<Incident> result = await _incidentService.GetIncidentsByCustomerId(customerId);

                foreach (var incident in result)
                {
                    var productId = new Guid(incident._productid_value);
                    var IncidentProduct = await ((IProductsService)_productsService).GetProductsByProductId(productId);
                   
                    var responseIncident = incident;
                    if (responseIncident != null)
                    {
                        incident.name = IncidentProduct.name;
                        incident.price = IncidentProduct.price;
                        //incident.description = IncidentProduct.description;
                        incident.currentPrice = IncidentProduct.currentPrice;

                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error retrieving : " + ex.Message);
            }
        }
    }
}
