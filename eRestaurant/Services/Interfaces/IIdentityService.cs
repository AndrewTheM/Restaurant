using eRestaurant.DTO;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
