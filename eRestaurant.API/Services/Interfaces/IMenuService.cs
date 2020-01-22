using eRestaurant.API.DTO;
using eRestaurant.API.Helpers;
using eRestaurant.Shared.DTO;
using System.Threading.Tasks;

namespace eRestaurant.API.Services
{
    public interface IMenuService
    {
        PagedList<MenuItem> GetMenu(PagingParameters paging, FilteringParameters filter);
        MenuItem GetMenuItem(int id);
        Dish GetDish(int id);
        Task<int> CreateDish(Dish dish);
        Task UpdateDish(Dish dish);
        Task DeleteDish(int id);
    }
}
