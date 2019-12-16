using eRestaurant.DTO;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public interface IMenuService
    {
        IEnumerable<MenuItemResponse> GetMenu();
        MenuItemResponse GetMenuItem(int id);
    }
}
