using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.Context;
using AccountsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi.Repositories
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private  AccountsContext _accountsContext;

        public AccountRepository(AccountsContext accountsContext)
        {
            _accountsContext = accountsContext ?? throw new ArgumentNullException(nameof(accountsContext));
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountsContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountDetailsForAsync(int accountId)
        {
            return await _accountsContext.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            _accountsContext.Add(account);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            // _accountsContext.Update(account);
            return await SaveChangesAsync();
        }

        public async Task<bool> AccountExists(int accountId)
        {
            var account = await GetAccountDetailsForAsync(accountId);
            return account != null;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _accountsContext.SaveChangesAsync();
            return result > 0;
        }

        public void Dispose()
        {
            _accountsContext?.Dispose();
            _accountsContext = null;
            GC.SuppressFinalize(this);
        }
    }
}