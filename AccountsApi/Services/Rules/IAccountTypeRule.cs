using AccountsApi.Entities;

namespace AccountsApi.Services.Rules
{
    public interface IAccountTypeRule
    {
        bool DoesApply(decimal accountBalance);
        AccountType Execute();
    }
}