using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;

namespace BankingProjectMVC.Assemblers
{
    public class AccountAssembler
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly ICustomerService _customerService;


        public AccountAssembler(IAccountTypeService accountTypeService,ICustomerService customerService)
        {
            _accountTypeService = accountTypeService;
            _customerService = customerService;
        }
        public Account ConvertToModel(AccountVM accountVM)
        {
            var accountype = _accountTypeService.GetById(accountVM.AccountTypeId);
            var customer = _customerService.GetById(accountVM.CustomerId);
            return new Account()
            {
                Id = accountVM.Id,
                AccountNo = accountVM.AccountNo,
                AccountType = accountype,
                Balance = accountVM.Balance,
                Customer = customer,
                Status = accountVM.Status,
            };
        }
        public AccountVM ConvertToViewModel(Account account)
        {
            return new AccountVM()
            {
                Id = account.Id,
                AccountNo = account.AccountNo,
                AccountTypeId = account.AccountType.Id,
                Balance = account.Balance,
                CustomerId = account.Customer.Id,
                TransactionsCount = account.Transactions != null ? account.Transactions.Count : 0,
                Status = account.Status,
            };
        }
    }
}