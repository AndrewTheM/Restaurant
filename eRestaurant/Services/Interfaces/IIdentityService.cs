using eRestaurant.API.DTO;
using System.Threading.Tasks;

namespace eRestaurant.API.Services
{
    public interface IIdentityService
    {
        Task<AuthResponse> RegisterAsync(string email, string password);
        Task<AuthResponse> LoginAsync(string email, string password);
    }
}
