using System.Collections.Generic;
using System.Linq;

namespace eRestaurant.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Remove(T item);
    }
}
