using AccountsApi.Entities;

namespace AccountsApi.Services.Rules
{
    public class BronzeAccountTypeRule : IAccountTypeRule
    {
        public bool DoesApply(decimal accountBalance) => accountBalance > 50000 && accountBalance < 100000;

        public AccountType Execute() => AccountType.Bronze;
    }
}