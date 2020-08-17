using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AccountsApi.Services
{
    public class NewBalanceChecker
    {
        public bool Process(decimal accountBalance)
        {
            throw new NotImplementedException();
        }
    }

    public class UnderTenRule : IBalanceCheckerRule
    {
        public bool DoesApply(decimal balance)
        {
            return balance < 10;
        }



    }

    public interface IBalanceCheckerRule
    {
    }
}
