using Microsoft.AspNetCore.Mvc;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductsService _productsService;


        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<Product> result = await _productsService.GetAllProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error retrieving products: " + ex.Message);
            }
        }


        // GET api/products/{productid}
        [HttpGet("{productid}")]
        public async Task<ActionResult<Product>> GetProductsByProductId(Guid productid)
        {
            try
            {
                Product product = await _productsService.GetProductsByProductId(productid);

                if (product == null)
                {
                    return NotFound(); // Return 404 Not Found if no products are found for the given account ID.
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the retrieval process.
                // Log the error and return an appropriate response.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
