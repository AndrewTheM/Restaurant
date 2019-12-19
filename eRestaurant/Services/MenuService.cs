using AutoMapper;
using eRestaurant.DTO;
using eRestaurant.Extensions;
using eRestaurant.Helpers;
using eRestaurant.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace eRestaurant.Services
{
    public class MenuService : IMenuService
    {
        private readonly IDishRepository _repo;
        private readonly IMapper _mapper;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = unitOfWork.Dishes;
            _mapper = mapper;
        }

        public MenuItemResponse GetMenuItem(int id)
        {
            var dish = _repo.Get(id);

            if (dish == null)
                return null;

            var unit = _repo.GetUnit(id);
            var type = _repo.GetType(id);
            var image = _repo.GetFirstImage(id);

            var response = _mapper.Map<MenuItemResponse>(dish, unit, type, image);
            response.Rating = _repo.CalculateAvgRating(id);
            return response;
        }

        public PagedList<MenuItemResponse> GetMenu(PagingParameters pars)
        {
            var menu = new List<MenuItemResponse>();
            var dishes = _repo.GetAll().ToList();
            foreach (var dish in dishes)
            {
                var item = GetMenuItem(dish.Id);
                menu.Add(item);
            }
            return menu.ToPagedList(pars);
        }
    }
}
