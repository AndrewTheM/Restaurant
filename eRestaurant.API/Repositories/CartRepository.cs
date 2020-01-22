using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eRestaurant.API.Context;
using eRestaurant.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.API.Repositories
{
    public class CartRepository : Repository<OrderCart>, ICartRepository
    {
        public CartRepository(ApplicationContext context) : base(context) { }

        public IEnumerable<int> GetDishesIds(int id) =>
            GetAll()
            .Include(oc => oc.CartDishes)
            .AsNoTracking()
            .SingleOrDefault(oc => oc.Id == id)
            .CartDishes
            .Select(cd => cd.DishId);
    }
}
