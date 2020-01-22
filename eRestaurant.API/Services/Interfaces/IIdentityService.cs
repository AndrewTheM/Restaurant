using eRestaurant.Shared.DTO;
using System.Threading.Tasks;

namespace eRestaurant.API.Services
{
    public interface IIdentityService
    {
        Task<TokenInfo> RegisterAsync(string email, string password);
        Task<TokenInfo> LoginAsync(string email, string password);
    }
}
