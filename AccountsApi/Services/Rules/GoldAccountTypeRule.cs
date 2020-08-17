using AccountsApi.Entities;

namespace AccountsApi.Services.Rules
{
    public class GoldAccountTypeRule : IAccountTypeRule
    {
        public bool DoesApply(decimal accountBalance) => accountBalance > 100000;

        public AccountType Execute() => AccountType.Gold;
    }
}