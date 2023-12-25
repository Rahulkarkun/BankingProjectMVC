using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProjectMVC.Repository
{
    public interface IAccountTypeRepository
    {
        string Add(AccountType accountType);
        string Update(AccountType accountType);
        string Delete(AccountType accountType);
        AccountType GetById(int accountTypeId);
        List<AccountType> GetAll();
    }
}
