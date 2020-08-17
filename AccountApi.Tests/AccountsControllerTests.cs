using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AccountsApi.Controllers;
using AccountsApi.Dto;
using AccountsApi.Entities;
using AccountsApi.Profiles;
using AccountsApi.Repositories;
using AccountsApi.Services.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AccountApi.Tests
{
    [TestFixture]
    public class AccountsControllerTests
    {
        private Mock<IAccountRepository> _accountRepository;
        private Mock<IAccountAddressService> _accountAddressService;
        private IMapper _mapper;
        private AccountsController _accountsController;

        [SetUp]
        public void Setup()
        {
             _accountRepository = new Mock<IAccountRepository>();
             _accountAddressService = new Mock<IAccountAddressService>();
            var mockMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AccountMappingProfile());
            });
            _mapper = mockMapperConfiguration.CreateMapper();

            _accountsController = new AccountsController(_accountRepository.Object, _accountAddressService.Object, _mapper);
        }

        [Test]
        public void GetAllAccounts_Action()
        {
            //arrange
            var accountEntitiesAccounts = new List<Account>()
            {
                new Account()
                {
                    Id = 1,Balance = 50000,FirstName = "firstname", LastName = "lastname", Type = AccountType.Silver
                },
                new Account()
                {
                    Id = 2,Balance = 90000,FirstName = "firstname1", LastName = "lastname1", Type = AccountType.Bronze
                }, 
                new Account()
                {
                    Id = 3,Balance = 110000,FirstName = "firstname2", LastName = "lastname2", Type = AccountType.Gold
                },

            };
            _accountRepository.Setup(x =>  x.GetAllAccountsAsync()).Returns(Task.FromResult<IEnumerable<Account>>(accountEntitiesAccounts));
            var accountAddress = "PostCode: ub79dn, City: London";
            _accountAddressService.Setup(x => x.GetAddress()).Returns(Task.FromResult(accountAddress));

            //act
            var actionResult = _accountsController.GetAllAccountsAsync().GetAwaiter().GetResult() as OkObjectResult;

            //assert

            actionResult.Should().NotBe(null);
            actionResult.StatusCode.Should().Be(200);
            ((IEnumerable<AccountDto>) actionResult.Value).Count().Should().Be(3);
            ((IEnumerable<AccountDto>) actionResult.Value).First().Address.Should().Be(accountAddress);
            

        }
        
        [Test]
        public void HappyPath_CreateAction()
        {
            //arrange
           
            _accountRepository.Setup(x =>  x.CreateAccountAsync(It.IsAny<Account>())).Returns(Task.FromResult(true));

            //act
            var accountDto = new AccountDto();
            var actionResult = _accountsController.CreateAccountAsync(accountDto).GetAwaiter().GetResult() as CreatedAtActionResult;

            //assert

            actionResult.Should().NotBe(null);
            actionResult.StatusCode.Should().Be(201);

        }
        
        [Test]
        public void HappyPath_UpdateAction()
        {
            //arrange
           
            _accountRepository.Setup(x =>  x.UpdateAccountAsync(It.IsAny<Account>())).Returns(Task.FromResult(true));
            _accountRepository.Setup(r => r.AccountExists(It.IsAny<int>())).Returns(Task.FromResult(true));

            //act
            var accountDto = new AccountDto() { Id = 1, Balance = 50000, FirstName = "firstname", LastName = "lastname", Type = AccountType.Silver };
            var actionResult = _accountsController.UpdateAccountAsync(1, accountDto).GetAwaiter().GetResult() as CreatedAtActionResult;

            //assert

            actionResult.Should().NotBe(null);
            actionResult.StatusCode.Should().Be(201);

        }

        [Test]
        public void UnHappyPath_UpdateAction_Returns_NotFoundResult()
        {
            //arrange
           
            _accountRepository.Setup(r => r.AccountExists(It.IsAny<int>())).Returns(Task.FromResult(false));

            //act
            var accountDto = new AccountDto() { Id = 1, Balance = 50000, FirstName = "firstname", LastName = "lastname", Type = AccountType.Silver };
            var actionResult = _accountsController.UpdateAccountAsync(1, accountDto).GetAwaiter().GetResult() as NotFoundResult;

            //assert

            actionResult.Should().NotBe(null);
            actionResult.StatusCode.Should().Be(404);
            _accountRepository.Verify(r => r.UpdateAccountAsync(It.IsAny<Account>()),Times.Never);

        }
        
        [Test]
        public void UnHappyPath_UpdateAction_Returns_BadRequestResult()
        {
            //arrange
           
            _accountRepository.Setup(r => r.AccountExists(It.IsAny<int>())).Returns(Task.FromResult(true));

            _accountsController.ModelState.AddModelError("FirstName", "The FirstName field is required."); 


            //act
            var accountDto = new AccountDto() { Id = 1, Balance = 50000,  LastName = "lastname", Type = AccountType.Silver };
            var actionResult = _accountsController.UpdateAccountAsync(1, accountDto).GetAwaiter().GetResult() as BadRequestObjectResult;

            

            //assert

            actionResult.Should().NotBe(null);
            actionResult.StatusCode.Should().Be(400);
            _accountRepository.Verify(r => r.UpdateAccountAsync(It.IsAny<Account>()),Times.Never);

        }
    }
}
