using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sqli_CRM_Web_Application.Connection;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dynamics365Controller : ControllerBase
    {
        private readonly Dynamics365Connection _connection;
        public Dynamics365Controller(Dynamics365Connection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAccessToken()
        {
            return    Ok(await _connection.GetAccessTokenAsync());
        }
    }
}
