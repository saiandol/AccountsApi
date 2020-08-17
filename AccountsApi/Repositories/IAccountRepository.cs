using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.Entities;

namespace AccountsApi.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();

        Task<Account> GetAccountDetailsForAsync(int accountId);
        Task<bool> CreateAccountAsync(Account account);
        Task<bool> UpdateAccountAsync(Account account);
        Task<bool> AccountExists(int accountId);
        Task<bool> SaveChangesAsync();
    }
}