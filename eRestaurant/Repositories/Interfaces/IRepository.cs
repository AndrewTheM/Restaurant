using System.Collections.Generic;

namespace eRestaurant.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Remove(T item);
    }
}
