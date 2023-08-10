using Microsoft.AspNetCore.Mvc;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            List<Account> accounts = await accountService.GetAccounts();
            return Ok(accounts);
        }

        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetAccountById(Guid accountid)
        {
            Account account = await accountService.GetAccountById(accountid);
            return Ok(account);
        }

        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetAccountNameById(Guid accountid)
        {
            string account = await accountService.GetAccountNameById(accountid);
            return Ok(account);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAccountByName(string name)
        {
            Account account = await accountService.GetAccountByName(name);
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(RequestAccount account)
        {
            bool isAdded = await accountService.AddAccount(account);
            return Ok(isAdded);
        }

        [HttpPatch("{accountid}")]
        public async Task<IActionResult> UpdateAccount(Guid accountid, [FromBody] RequestAccount account)
        {
            bool isUpdated = await accountService.UpdateAccount(accountid, account);
            return Ok(isUpdated);
        }

        [HttpPatch("{accountid}")]
        public async Task<IActionResult> ToggleStatus(Guid accountid, [FromBody] StatusRequest statusRequest)
        {
            bool isUpdated = await accountService.ToggleStatus(accountid, statusRequest);
            return Ok(isUpdated);
        }

        [HttpDelete("{accountid}")]
        public async Task<IActionResult> DeleteAccount(Guid accountid)
        {
            bool isDeleted = await accountService.DeleteAccount(accountid);
            return Ok(isDeleted);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccounts(string[] accountsIds)
        {
            await accountService.DeleteAccounts(accountsIds);
            return Ok();
        }
    }
}
