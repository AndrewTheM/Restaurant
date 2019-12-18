using eRestaurant.Entities;
using System.Collections.Generic;
using System.Linq;

namespace eRestaurant.Repositories
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(ApplicationContext context) : base(context) { }

        public double CalculateAvgRating(int id) => _context.Reviews.Where(r => r.DishId == id).Average(r => r.Rating);

        public IEnumerable<Dish> GetByTypeName(string typeName) => GetAll().Where(d => d.Type.Name == typeName);

        public IEnumerable<Dish> GetHighestRatedOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.Reviews.Average(r => r.Rating)).Take(count);

        public IEnumerable<byte[]> GetImages(int id) => _context.Images.Where(di => di.DishId == id).Select(di => di.Image);

        public IEnumerable<Dish> GetMostPopularOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.OrderDishes.Count()).Take(count);

        public string GetTypeName(int id)
        {
            var typeId = Get(id).TypeId;
            return _context.DishTypes.Where(dt => dt.Id == typeId).FirstOrDefault()?.Name;
        }

        public string GetUnitName(int id)
        {
            var unitId = Get(id).UnitId;
            return _context.Units.Where(u => u.Id == unitId).FirstOrDefault()?.Name;
        }
    }
}
