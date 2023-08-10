using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public interface IAccountService
    {
        public Task<List<Account>> GetAccounts();
        public Task<Account> GetAccountById(Guid accountid);
        public Task<string> GetAccountNameById(Guid accountid);
        public Task<Account> GetAccountByName(string fullname);
        public Task<bool> AddAccount(RequestAccount account);
        public Task<bool> UpdateAccount(Guid accountid, RequestAccount account);
        public Task<bool> ToggleStatus(Guid accountid, StatusRequest statusRequest);
        public Task<bool> DeleteAccount(Guid accountid);
        public Task DeleteAccounts(string[] accountsIds);



    }
}
