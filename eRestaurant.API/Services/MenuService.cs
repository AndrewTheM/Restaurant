﻿using AutoMapper;
using eRestaurant.API.DTO;
using eRestaurant.API.Entities;
using eRestaurant.API.Extensions;
using eRestaurant.API.Helpers;
using eRestaurant.API.Repositories;
using SharedDto = eRestaurant.Shared.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.API.Services
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

        public SharedDto.MenuItem GetMenuItem(int id)
        {
            var dish = _repo.Get(id);

            if (dish == null)
                return null;

            var unit = _repo.GetUnitOfDish(id);
            var type = _repo.GetTypeOfDish(id);
            var image = _repo.GetFirstImage(id);

            var menuItem = _mapper.Map<SharedDto.MenuItem>(dish, unit, type, image);
            menuItem.Rating = _repo.CalculateAvgRating(id);
            return menuItem;
        }

        public PagedList<SharedDto.MenuItem> GetMenu(PagingParameters paging, FilteringParameters filter)
        {
            List<Dish> dishes;

            if (string.IsNullOrWhiteSpace(filter.Name))
                if (string.IsNullOrWhiteSpace(filter.Category))
                    dishes = _repo.GetAll().ToList();
                else
                    dishes = _repo.GetByTypeName(filter.Category).ToList();
            else
                if (string.IsNullOrWhiteSpace(filter.Category))
                    dishes = _repo.GetWhereNameContains(filter.Name).ToList();
                else
                    dishes = _repo.GetWhereNameContainsOfType(filter.Name, filter.Category).ToList();

            var menu = new List<SharedDto.MenuItem>();
            foreach (var dish in dishes)
            {
                var item = GetMenuItem(dish.Id);
                menu.Add(item);
            }
            return menu.ToPagedList(paging);
        }

        public SharedDto.Dish GetDish(int id)
        {
            var dish = _repo.Get(id);
            var dishDto = _mapper.Map<SharedDto.Dish>(dish);
            return dishDto;
        }

        public async Task<int> CreateDish(SharedDto.Dish dishReq)
        {
            var dish = _mapper.Map<Dish>(dishReq);
            _repo.Add(dish);
            await _uow.SaveChangesAsync();

            int id = _repo.GetAll().ToList().Last().Id;
            return id;
        }

        public async Task UpdateDish(SharedDto.Dish dishReq)
        {
            var dish = _mapper.Map<Dish>(dishReq);
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
