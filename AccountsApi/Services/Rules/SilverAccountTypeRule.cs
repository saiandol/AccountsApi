using AccountsApi.Entities;

namespace AccountsApi.Services.Rules
{
    public class SilverAccountTypeRule : IAccountTypeRule
    {
        public bool DoesApply(decimal accountBalance) => accountBalance < 50000;

        public AccountType Execute() => AccountType.Silver;
    }
}