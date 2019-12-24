using AutoMapper;
using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Extensions;
using eRestaurant.Helpers;
using eRestaurant.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDishRepository _repo;
        private readonly IMapper _mapper;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _repo = unitOfWork.Dishes;
            _mapper = mapper;
        }

        public MenuItem GetMenuItem(int id)
        {
            var dish = _repo.Get(id);

            if (dish == null)
                return null;

            var unit = _repo.GetUnit(id);
            var type = _repo.GetType(id);
            var image = _repo.GetFirstImage(id);

            var response = _mapper.Map<MenuItem>(dish, unit, type, image);
            response.Rating = _repo.CalculateAvgRating(id);
            return response;
        }

        public PagedList<MenuItem> GetMenu(PagingParameters pars)
        {
            var menu = new List<MenuItem>();
            var dishes = _repo.GetAll().ToList();
            foreach (var dish in dishes)
            {
                var item = GetMenuItem(dish.Id);
                menu.Add(item);
            }
            return menu.ToPagedList(pars);
        }

        public async Task CreateDish(Dish dish)
        {
            _repo.Add(dish);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateDish(Dish dish)
        {
            _uow.ModifyState(dish);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteDish(int id)
        {
            _repo.Remove(new Dish { Id = id });
            await _uow.SaveChangesAsync();
        }
    }
}
