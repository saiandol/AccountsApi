using System;

namespace AccountsApi.Services
{
    public class BalanceChecker
    {
        private readonly Persistence _persistence;
        private readonly ExternalApi _externalApi;

        public BalanceChecker(Persistence persistence, ExternalApi externalApi)
        {
            _persistence = persistence;
            _externalApi = externalApi;
        }

        public bool Process(decimal amount, string aType)
        {
            if (amount < 10)
            {
                Process10();
                return true;
            }

            if (amount > 50 && DateTime.Now.Day > 15)
            {
                return _persistence.GetInfo();
            }

            if (amount > 100000)
            {
                return _externalApi.CheckAccountBalance(amount, aType);
            }

            return true;
        }


        private void Process10() => Console.WriteLine("less 10");
    }

    public class Persistence
    {
        public bool GetInfo()
        {
            return true;
        }
    }

    public class ExternalApi
    {
        public bool CheckAccountBalance(decimal amount, string accountType)
        {
            if (amount > 1000000 && accountType == "gold")
                return true;
            else return false;
        }
    }
}