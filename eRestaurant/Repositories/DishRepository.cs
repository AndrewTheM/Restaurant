using eRestaurant.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace eRestaurant.Repositories
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(ApplicationContext context) : base(context) { }

        public double CalculateAvgRating(int id)
        {
            var reviews =
                GetAll()
                .Include(d => d.Reviews)
                .AsNoTracking()
                .SingleOrDefault(d => d.Id == id)
                .Reviews;

            if (reviews.Any())
                return reviews.Average(r => r.Rating);
            return 0;
        }

        public IEnumerable<Dish> GetByTypeName(string typeName) =>
            GetAll()
            .Include(d => d.Type)
            .Where(d => d.Type.Name == typeName)
            .AsNoTracking();

        public IEnumerable<Dish> GetWhereNameContains(string name) =>
            GetAll()
            .Where(d => d.Name.Contains(name))
            .AsNoTracking();

        public IEnumerable<Dish> GetWhereNameContainsOfType(string name, string typeName) =>
            GetAll()
            .Include(d => d.Type)
            .Where(d => d.Type.Name == typeName && d.Name.Contains(name))
            .AsNoTracking();

        public DishImage GetFirstImage(int id) =>
            GetAll()
            .Include(d => d.Images)
            .AsNoTracking()
            .SingleOrDefault(d => d.Id == id)
            .Images
            .FirstOrDefault();

        public IEnumerable<Dish> GetHighestRatedOfType(string typeName, int count) =>
            GetByTypeName(typeName)
            .OrderByDescending(d => d.Reviews.Average(r => r.Rating))
            .Take(count);

        public IEnumerable<byte[]> GetImageCodes(int id) =>
            GetAll()
            .Include(d => d.Images)
            .AsNoTracking()
            .SingleOrDefault(d => d.Id == id)
            .Images
            .Select(di => di.Image);

        public IEnumerable<Dish> GetMostPopularOfType(string typeName, int count) =>
            GetByTypeName(typeName)
            .OrderByDescending(d => d.OrderDishes.Count())
            .Take(count);

        public DishType GetTypeOfDish(int id) =>
            GetAll()
            .Include(d => d.Type)
            .AsNoTracking()
            .SingleOrDefault(d => d.Id == id)
            .Type;

        public UnitOfMeasurement GetUnitOfDish(int id) =>
            GetAll()
            .Include(d => d.PortionUnit)
            .AsNoTracking()
            .SingleOrDefault(d => d.Id == id)
            .PortionUnit;
    }
}
