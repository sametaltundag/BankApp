using BankApp.Web.Data.Entities;
using System.Collections.Generic;

namespace BankApp.Web.Data.Interfaces
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> GetAll();
        ApplicationUser GetById(int id);
    }
}
