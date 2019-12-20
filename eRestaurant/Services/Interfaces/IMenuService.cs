using eRestaurant.DTO;
using eRestaurant.Helpers;
using System.Collections.Generic;

namespace eRestaurant.Services
{
    public interface IMenuService
    {
        PagedList<MenuItem> GetMenu(PagingParameters pars);
        MenuItem GetMenuItem(int id);
        void CreateDish(MenuItem request); // + Request
    }
}
