using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Assemblers
{
    public class AccountTypeAssembler
    {
        
        public AccountType ConvertToModel(AccountTypeVM accountTypeVM)

        {
            return new AccountType()
            {
                Id = accountTypeVM.Id,
                Type = accountTypeVM.Type,
                //AccountsCount = accountTypeVM.AccountsCount,
            };


        }
        public AccountTypeVM ConvertToViewModel(AccountType accountType)
        {
            return new AccountTypeVM()
            {
                Id = accountType.Id,
                Type = accountType.Type,
                AccountsCount = accountType.Accounts != null ? accountType.Accounts.Count : 0,
            };
        }
    }
}