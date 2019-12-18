using eRestaurant.DTO;
using System.Collections.Generic;

namespace eRestaurant.Services
{
    public interface IMenuService
    {
        IEnumerable<MenuItemResponse> GetMenu();
        MenuItemResponse GetMenuItem(int id);
    }
}
