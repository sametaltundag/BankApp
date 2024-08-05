using BankApp.Web.Data.Context;
using BankApp.Web.Data.Interfaces;
using BankApp.Web.Data.Mapping;
using BankApp.Web.Data.Repositories;
using BankApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserMapper _userMapper;
        private readonly IAccountMapper _accountMapper;



        public AccountController(IUserMapper userMapper, IApplicationUserRepository applicationUserRepository, IAccountRepository accountRepository, IAccountMapper accountMapper)
        {
            _userMapper = userMapper;
            _applicationUserRepository = applicationUserRepository;
            _accountRepository = accountRepository;
            _accountMapper = accountMapper;
        }

        public IActionResult Create(int id)
        {
            var userInfo = _userMapper.MapToUserList(_applicationUserRepository.GetById(id));
            return View(userInfo);
        }

        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _accountRepository.Create(_accountMapper.Map(model));
            return RedirectToAction("Index","Home");
        }
    }
}
