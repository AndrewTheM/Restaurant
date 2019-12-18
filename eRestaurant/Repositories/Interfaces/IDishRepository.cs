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
        string GetTypeName(int id);
        string GetUnitName(int id);
        IEnumerable<byte[]> GetImages(int id);
        double CalculateAvgRating(int id);
    }
}
