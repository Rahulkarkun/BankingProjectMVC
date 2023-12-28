using BankingProjectMVC.Models;
using BankingProjectMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public string Add(Account account)
        {
            return _accountRepository.Add(account);
        }
        public string Update(Account account)
        {
            return _accountRepository.Update(account);
        }
        public string Delete(Account account)
        {
            return _accountRepository.Delete(account);
        }
        public Account GetById(int accountId)
        {
            return _accountRepository.GetById(accountId);
        }
        public List<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public Account GetByAccountNumber(string accNo)
        {
            return _accountRepository.GetByAccountNumber(accNo);
        }
    }
}