using System;
using System.Collections.Generic;
using System.Text;
using AccountsApi.Entities;
using AccountsApi.Services;
using AccountsApi.Services.Interfaces;
using AccountsApi.Services.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace AccountApi.Tests
{
    [TestFixture]
    public class AccountTypeEvaluatorTests
    {
        private IAccountTypeEvaluator _accountTypeEvaluator;

        [SetUp]
        public void Setup()
        {
            IEnumerable<IAccountTypeRule> accountTypeRules = new List<IAccountTypeRule>()
            {
                new SilverAccountTypeRule(),
                new BronzeAccountTypeRule(),
                new GoldAccountTypeRule()
            };

            _accountTypeEvaluator = new AccountTypeEvaluator(accountTypeRules);
        }

        [TestCase(49000, ExpectedResult = AccountType.Silver)]
        [TestCase(51000, ExpectedResult = AccountType.Bronze)]
        [TestCase(101000, ExpectedResult = AccountType.Gold)]
        public AccountType Evaluate_GivenAnBalance_ReturnsItsAccountType(decimal accountBalance)
        {
            return  _accountTypeEvaluator.Evaluate(accountBalance);
        }
    }
}
