using eRestaurant.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRestaurant.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        IEnumerable<Dish> GetByTypeName(string typeName);
        IEnumerable<Dish> GetMostPopularOfType(string typeName, int count);
        IEnumerable<Dish> GetHighestRatedOfType(string typeName, int count);
        DishType GetType(int id);
        UnitOfMeasurement GetUnit(int id);
        DishImage GetFirstImage(int id);
        IEnumerable<byte[]> GetImageCodes(int id);
        double CalculateAvgRating(int id);
    }
}
