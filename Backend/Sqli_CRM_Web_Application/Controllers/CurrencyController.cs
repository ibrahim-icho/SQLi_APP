using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("{transactioncurrencyid}")]
        async public Task<IActionResult> GetCurrency(Guid transactioncurrencyid)
        {
            try
            {
                TransactionCurrency currency = await _currencyService.GetCurrency(transactioncurrencyid);

                if (currency ==  null)
                {
                    return NotFound();
                }

                return Ok(currency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
