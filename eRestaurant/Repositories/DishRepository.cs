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

        public IEnumerable<Dish> GetByTypeName(string typeName) => GetAll().Where(d => d.Type.Name == typeName);

        public IEnumerable<Dish> GetHighestRatedOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.Reviews.Average(r => r.Rating)).Take(count);

        public IEnumerable<byte[]> GetImages(int id)
        {
            var dishes = GetAll();
            return _context.Images.Join(dishes,
                                        di => di.DishId,
                                        d => d.Id,
                                        (di, d) => di.Image);
        }

        public IEnumerable<Dish> GetMostPopularOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.OrderDishes.Count()).Take(count);

        public string GetTypeName(int id)
        {
            var dishes = GetAll();
            return _context.DishTypes.Join(dishes, 
                                           dt => dt.Id,
                                           d => d.TypeId,
                                           (dt, d) => dt.Name).FirstOrDefault();
        }

        public string GetUnitName(int id)
        {
            var dishes = GetAll();
            return _context.Units.Join(dishes,
                                       u => u.Id,
                                       d => d.UnitId,
                                       (u, d) => u.Name).FirstOrDefault();
        }
    }
}
