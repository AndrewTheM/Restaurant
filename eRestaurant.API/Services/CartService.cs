using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eRestaurant.API.Entities;
using eRestaurant.API.Repositories;
using SharedDto = eRestaurant.Shared.DTO;

namespace eRestaurant.API.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<SharedDto.Dish> GetCartDishes(int id)
        {
            var ids = _uow.Carts.GetDishesIds(id);
            IEnumerable<SharedDto.Dish> dishes = new List<SharedDto.Dish>();
            
            foreach (var dishId in ids)
            {
                var dish = _uow.Dishes.Get(dishId);
                var dto = _mapper.Map<SharedDto.Dish>(dish);
                dishes.Append(dto);
            }
            return dishes;
        }

        public Task AddDishToCart(int cartId, Dish dish)
        {
            throw new NotImplementedException();
        }


        public Task RemoveDishFromCart(int cartId, int dishId)
        {
            throw new NotImplementedException();
        }
    }
}
