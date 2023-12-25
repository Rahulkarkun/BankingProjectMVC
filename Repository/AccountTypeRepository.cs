using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Repository
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        public string Add(AccountType accountType)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(accountType);
                    txn.Commit();
                }
            }
            return "Added Successfully";
        }

        public string Update(AccountType accountType)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(accountType);
                    txn.Commit();
                }
            }
            return "Updated Successfully";
        }

        public string Delete(AccountType accountType)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Delete(accountType);
                    txn.Commit();
                }
            }
            return "Deleted Successfully";
        }

        public AccountType GetById(int accountTypeId)
        {
            AccountType accountType = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    accountType = session.Load<AccountType>(accountTypeId);
                    txn.Commit();
                }
            }
            return accountType;
        }

        public List<AccountType> GetAll()
        {
            List<AccountType> accountType = new List<AccountType>() { };
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    accountType = session.Query<AccountType>()
                    .Fetch(at => at.Accounts).ToList();
                    txn.Commit();
                }
            }
            return accountType;
        }

    }
}