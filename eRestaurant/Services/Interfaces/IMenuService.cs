using eRestaurant.DTO;
using eRestaurant.Helpers;
using System.Collections.Generic;

namespace eRestaurant.Services
{
    public interface IMenuService
    {
        PagedList<MenuItemResponse> GetMenu(PagingParameters pars);
        MenuItemResponse GetMenuItem(int id);
    }
}
