using BankApp.Web.Data.Entities;
using BankApp.Web.Models;

namespace BankApp.Web.Data.Interfaces
{
    public interface IAccountMapper
    {
        public Account Map(AccountCreateModel model);
    }
}
