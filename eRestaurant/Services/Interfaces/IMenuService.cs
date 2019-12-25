using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Helpers;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public interface IMenuService
    {
        PagedList<MenuItem> GetMenu(PagingParameters pars);
        MenuItem GetMenuItem(int id);
        Task CreateDish(DishRequest dish);
        Task UpdateDish(DishRequest dish);
        Task DeleteDish(int id);
    }
}
