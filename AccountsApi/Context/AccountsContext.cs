using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi.Context
{
    public class AccountsContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account()
                {
                    Id = 1,
                    FirstName = "Matt",
                    LastName = "Black",
                    Balance = 41000,
                    Type = AccountType.Silver
                },
                new Account()
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Smith",
                    Balance = 52000,
                    Type = AccountType.Bronze
                },
                new Account()
                {
                    Id = 3,
                    FirstName = "Ben",
                    LastName = "Aston",
                    Balance = 104000,
                    Type = AccountType.Gold
                },
                new Account()
                {
                    Id = 4,
                    FirstName = "Ben",
                    LastName = "Smith",
                    Balance = 50000,
                    Type = AccountType.Silver
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
