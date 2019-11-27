using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eRestaurant.Repositories
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(ApplicationContext context) : base(context) { }

        public ApplicationContext Context => _context as ApplicationContext;

        public IEnumerable<Dish> GetByTypeName(string typeName) => GetAll().Where(d => d.Type.Name == typeName);

        public IEnumerable<Dish> GetHighestRatedOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.Reviews.Average(r => r.Rating)).Take(count);

        public IEnumerable<Dish> GetMostPopularOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.OrderDishes.Count()).Take(count);
    }
}
