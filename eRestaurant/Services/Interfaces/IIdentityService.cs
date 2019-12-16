using eRestaurant.DTO;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public interface IIdentityService
    {
        Task<AuthResponse> RegisterAsync(string email, string password);
        Task<AuthResponse> LoginAsync(string email, string password);
    }
}
