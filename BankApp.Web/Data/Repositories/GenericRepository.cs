using BankApp.Web.Data.Context;
using BankApp.Web.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Web.Data.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class, new()
    {
        private readonly BankContext _context;

        public GenericRepository(BankContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IQueryable<T> GetQuaryable()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
