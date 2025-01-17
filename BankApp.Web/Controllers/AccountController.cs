﻿using BankApp.Web.Data.Context;
using BankApp.Web.Data.Entities;
using BankApp.Web.Data.Interfaces;
using BankApp.Web.Data.Mapping;
using BankApp.Web.Data.Repositories;
using BankApp.Web.Data.UnitOfWork;
using BankApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IAccountRepository _accountRepository;
        //private readonly IUserMapper _userMapper;
        //private readonly IAccountMapper _accountMapper;



        //public AccountController(IUserMapper userMapper, IApplicationUserRepository applicationUserRepository, IAccountRepository accountRepository, IAccountMapper accountMapper)
        //{
        //    _userMapper = userMapper;
        //    _applicationUserRepository = applicationUserRepository;
        //    _accountRepository = accountRepository;
        //    _accountMapper = accountMapper;
        //}

        //private readonly IGenericRepository<Account> _accountRepository;
        //private readonly IGenericRepository<ApplicationUser> _userRepository;

        //public AccountController(IGenericRepository<Account> accountRepository, IGenericRepository<ApplicationUser> userRepository)
        //{
        //    _accountRepository = accountRepository;
        //    _userRepository = userRepository;
        //}

        private readonly IUow _uow;

        public IActionResult Create(int id)
        {
            var userInfo = _uow.GetGenericRepository<ApplicationUser>().GetById(id);
            return View(new UserListModel
            {
                Id = userInfo.Id,
                Name = userInfo.Name,
                Surname = userInfo.Surname
            });
        }

        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _uow.GetGenericRepository<Account>().Create(new Account
            {
                AccountNumber = model.AccountNumber,
                Balance = model.Balance,
                ApplicationUserId = model.ApplicationUserId
            });
            _uow.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var query = _uow.GetGenericRepository<Account>().GetQuaryable();
            var accountList = query.Where(x => x.ApplicationUserId == userId).ToList();
            var user = _uow.GetGenericRepository<ApplicationUser>().GetById(userId);


            ViewBag.FullName = user.Name + " " + user.Surname;

            var list = new List<AccountListModel>();

            foreach (var account in accountList)
            {
                list.Add(new()
                {
                    ApplicationUserId = account.ApplicationUserId,
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _uow.GetGenericRepository<Account>().GetQuaryable();
            var accounts = query.Where(x=> x.Id != accountId).ToList();
            var list = new List<AccountListModel>();
            
            ViewBag.SenderId = accountId;

            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }

            return View(new SelectList(list,"Id","AccountNumber"));
        }

        [HttpPost]
        public IActionResult SendMoney(SendMoneyModel model)
        {
            // Unit Of Work

            var senderAccount = _uow.GetGenericRepository<Account>().GetById(model.SenderId);
            senderAccount.Balance -= model.Amount;
            _uow.GetGenericRepository<Account>().Update(senderAccount);

            var account = _uow.GetGenericRepository<Account>().GetById(model.AccountId);
            account.Balance += model.Amount;
            _uow.GetGenericRepository<Account>().Update(account);

            _uow.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}
