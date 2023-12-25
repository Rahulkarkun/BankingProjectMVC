using BankingProjectMVC.Models;
using BankingProjectMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Services
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        public AccountTypeService(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }
        public string Add(AccountType accountType)
        {
            return _accountTypeRepository.Add(accountType);
        }
        public string Update(AccountType accountType)
        {
            return _accountTypeRepository.Update(accountType);
        }
        public string Delete(AccountType accountType)
        {
            return _accountTypeRepository.Delete(accountType);
        }
        public AccountType GetById(int accountTypeId)
        {
            return _accountTypeRepository.GetById(accountTypeId);
        }
        public List<AccountType> GetAll()
        {
            return _accountTypeRepository.GetAll();
        }
    }
}