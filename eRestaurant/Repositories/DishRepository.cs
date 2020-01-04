using eRestaurant.Entities;
using System.Collections.Generic;
using System.Linq;

namespace eRestaurant.Repositories
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(ApplicationContext context) : base(context) { }

        public double CalculateAvgRating(int id)
        {
            var reviews = _context.Reviews?.Where(r => r.DishId == id);
            if (reviews.Any())
                return reviews.Average(r => r.Rating);
            return 0;
        }

        public IEnumerable<Dish> GetByTypeName(string typeName) => GetAll().Where(d => d.Type.Name == typeName);

        public IEnumerable<Dish> GetWhereNameContains(string name) => GetAll().Where(d => d.Name.Contains(name));

        public DishImage GetFirstImage(int id) => _context.Images.Where(di => di.DishId == id).FirstOrDefault();

        public IEnumerable<Dish> GetHighestRatedOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.Reviews.Average(r => r.Rating)).Take(count);

        public IEnumerable<byte[]> GetImageCodes(int id) => _context.Images.Where(di => di.DishId == id).Select(di => di.Image);

        public IEnumerable<Dish> GetMostPopularOfType(string typeName, int count) => GetByTypeName(typeName).OrderByDescending(d => d.OrderDishes.Count()).Take(count);

        public DishType GetType(int id)
        {
            var typeId = Get(id).TypeId;
            return _context.DishTypes.Where(dt => dt.Id == typeId).FirstOrDefault();
        }

        public UnitOfMeasurement GetUnit(int id)
        {
            var unitId = Get(id).UnitId;
            return _context.Units.Where(u => u.Id == unitId).FirstOrDefault();
        }

    }
}
