using AccountsApi.Entities;

namespace AccountsApi.Services.Interfaces
{
    public interface IAccountTypeEvaluator
    {
        AccountType Evaluate(decimal accountBalance);
    }
}