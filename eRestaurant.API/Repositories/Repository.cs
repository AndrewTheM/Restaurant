using eRestaurant.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eRestaurant.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _set;

        public Repository(ApplicationContext context) => _set = context.Set<T>();

        public IQueryable<T> GetAll() => _set;

        public T Get(int id) => _set.Find(id);

        public void Add(T item) => _set.Add(item);

        public void Remove(T item) => _set.Remove(item);
    }
}
