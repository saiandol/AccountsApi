using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AccountsApi.Entities;
using AccountsApi.Services.Interfaces;
using AccountsApi.Services.Rules;
using AutoMapper.Configuration;

namespace AccountsApi.Services
{
    public class AccountTypeEvaluator : IAccountTypeEvaluator
    {
        private readonly IEnumerable<IAccountTypeRule> _accountTypeRules;

        public AccountTypeEvaluator(IEnumerable<IAccountTypeRule> accountTypeRules)
        {
            _accountTypeRules = accountTypeRules ?? throw new ArgumentNullException();
        }
        public AccountType Evaluate(decimal accountBalance)
        {
            var accountTypeRule = _accountTypeRules.FirstOrDefault(rule => rule.DoesApply(accountBalance));
            return accountTypeRule?.Execute() ?? AccountType.Silver;
        }
    }
}
