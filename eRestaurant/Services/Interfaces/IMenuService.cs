using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Helpers;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public interface IMenuService
    {
        PagedList<MenuItem> GetMenu(PagingParameters paging, FilteringParameters filter);
        MenuItem GetMenuItem(int id);
        DishRequest GetDish(int id);
        Task<int> CreateDish(DishRequest dish);
        Task UpdateDish(DishRequest dish);
        Task DeleteDish(int id);
    }
}
