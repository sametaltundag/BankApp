using BankApp.Web.Data.Entities;

namespace BankApp.Web.Data.Interfaces
{
    public interface IAccountRepository
    {
        void Create(Account user);
    }
}
