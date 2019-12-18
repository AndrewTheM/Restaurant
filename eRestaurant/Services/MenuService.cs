using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace eRestaurant.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDishRepository _repo;

        public MenuService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _repo = _uow.Dishes;
        }

        public MenuItemResponse GetMenuItem(int id)
        {
            Dish dish = _repo.Get(id);
            string unit = _repo.GetUnitName(id);
            string type = _repo.GetTypeName(id);
            byte[] image = _repo.GetImages(id).FirstOrDefault();
            double rating = _repo.CalculateAvgRating(id);

            return new MenuItemResponse
            {
                Id = id,
                Name = dish.Name,
                Description = dish.Description,
                Price = $"{dish.Price:0.00}",
                Portion = $"{dish.PortionSize} {unit}",
                CookingTime = dish.CookingTime.ToString(),
                Type = type,
                Rating = rating,
                Image = image
            };
        }

        public IEnumerable<MenuItemResponse> GetMenu()
        {
            var menu = new List<MenuItemResponse>();
            var dishes = _repo.GetAll().ToList();
            foreach (var dish in dishes)
            {
                var item = GetMenuItem(dish.Id);
                menu.Add(item);
            }
            return menu;
        }
    }
}
