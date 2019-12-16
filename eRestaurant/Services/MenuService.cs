using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Repositories;

namespace eRestaurant.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _uow;

        public MenuService(IUnitOfWork unitOfWork) => _uow = unitOfWork;

        public MenuItemResponse GetMenuItem(int id)
        {
            Dish dish = _uow.Dishes.Get(id);
            string unit = _uow.Dishes.GetUnitName(id);
            string type = _uow.Dishes.GetTypeName(id);
            byte[] image = _uow.Dishes.GetImages(id).FirstOrDefault();

            return new MenuItemResponse
            {
                Id = id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Portion = $"{dish.PortionSize} {unit}",
                CookingTime = dish.CookingTime.ToString(),
                Type = type,
                Image = image
            };
        }

        public IEnumerable<MenuItemResponse> GetMenu()
        {
            var menu = new List<MenuItemResponse>();
            var dishes = _uow.Dishes.GetAll().ToList();
            foreach (var dish in dishes)
            {
                var item = GetMenuItem(dish.Id);
                menu.Add(item);
            }
            return menu;
        }
    }
}
