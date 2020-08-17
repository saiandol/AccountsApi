using System.Threading.Tasks;

namespace AccountsApi.Services.Interfaces
{
    public interface IAccountAddressService
    {
        Task<string> GetAddress();
    }
}