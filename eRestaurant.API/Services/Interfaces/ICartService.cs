using eRestaurant.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedDto = eRestaurant.Shared.DTO;

namespace eRestaurant.API.Services
{
    public interface ICartService
    {
        IEnumerable<SharedDto.Dish> GetCartDishes(int id);
        Task AddDishToCart(int cartId, Dish dish);
        Task RemoveDishFromCart(int cartId, int dishId);
    }
}
