using AccountsApi.Dto;
using AutoMapper;
using Account = AccountsApi.Entities.Account;

namespace AccountsApi.Profiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>().ForMember(d => d.Id, opt => opt.Ignore());
        }

    }
}
