using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AccountsApi.Dto;
using AccountsApi.Entities;
using AccountsApi.Repositories;
using AccountsApi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AccountsApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountAddressService _accountAddressService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepository accountRepository, IAccountAddressService accountAddressService, IMapper mapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException();
            _accountAddressService = accountAddressService ?? throw new ArgumentNullException();
            _mapper = mapper ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();

            var accountAddress = await _accountAddressService.GetAddress();

            var allAccounts = (from account in accounts
                let accountDto = new AccountDto() { Address = accountAddress }
                select _mapper.Map(account, accountDto)).ToList();

            return Ok(allAccounts);
           
        }
        
        [HttpGet( "{accountId}")]
        public async Task<IActionResult> GetAccountDetailsFor(int accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest();
            }
            var account = await _accountRepository.GetAccountDetailsForAsync(accountId);

            if (account == null)
            {
                return NotFound();
            }
            var accountDto = _mapper.Map<AccountDto>(account);

            accountDto.Address = await _accountAddressService.GetAddress();

            return Ok(accountDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountDto accountDto)
        {

            var account = _mapper.Map<Account>(accountDto);
            await _accountRepository.CreateAccountAsync(account);

            // return CreatedAtAction(nameof(GetAccountDetailsFor), accountDto.Id);
            // return CreatedAtRoute(nameof(GetAccountDetailsFor), accountDto.Id);
            return NoContent();
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> UpdateAccountAsync(int accountId, [FromBody] AccountDto accountDto)
        {
            if (!await _accountRepository.AccountExists(accountId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(accountDto);
            }

            var accountEntity = await _accountRepository.GetAccountDetailsForAsync(accountId);

            _mapper.Map(accountDto, accountEntity);
            await _accountRepository.SaveChangesAsync();

            return NoContent();

        }

        
    }
}
