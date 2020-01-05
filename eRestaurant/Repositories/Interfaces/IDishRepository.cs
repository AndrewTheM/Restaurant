using eRestaurant.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRestaurant.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        IEnumerable<Dish> GetByTypeName(string typeName);
        IEnumerable<Dish> GetWhereNameContains(string name);
        IEnumerable<Dish> GetWhereNameContainsOfType(string name, string typeName);
        IEnumerable<Dish> GetMostPopularOfType(string typeName, int count);
        IEnumerable<Dish> GetHighestRatedOfType(string typeName, int count);
        DishType GetTypeOfDish(int id);
        UnitOfMeasurement GetUnitOfDish(int id);
        DishImage GetFirstImage(int id);
        IEnumerable<byte[]> GetImageCodes(int id);
        double CalculateAvgRating(int id);
    }
}
