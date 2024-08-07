using BankApp.Web.Data.Context;
using BankApp.Web.Data.Interfaces;
using BankApp.Web.Data.Repositories;

namespace BankApp.Web.Data.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly BankContext _context;

        public Uow(BankContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetGenericRepository<T>() where T : class, new()
        {
            return new GenericRepository<T> (_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
            
    }
}
