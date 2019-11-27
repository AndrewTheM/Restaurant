using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eRestaurant.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        private readonly DbSet<T> _set;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _set;

        public T Get(int id) => _set.Find(id);

        public void Add(T item) => _set.Add(item);

        public void Remove(T item) => _set.Remove(item);
    }
}
