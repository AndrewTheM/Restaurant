using System.Threading.Tasks;

namespace eRestaurant.Blazor.Auth
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }
}