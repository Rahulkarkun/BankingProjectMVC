using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using BankingProjectMVC.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Repository
{
    public class AccountRepository:IAccountRepository
    {
        public string Add(Account account)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(account);
                    txn.Commit();

                }
            }
            return "Added Succesfully";
        }
        public string Update(Account account)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(account);
                    txn.Commit();

                }
            }
            return "Updated Succesfully";
        }
        public string Delete(Account account)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Delete(account);
                    txn.Commit();

                }
            }
            return "Deleted Succesfully";
        }
        public Account GetById(int accountId)
        {
            Account account = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    account = session.Query<Account>()
                        .Where(c => c.Id == accountId)
                        .FirstOrDefault();
                    txn.Commit();

                }
            }
            return account;
        }
        public List<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    accounts = session.Query<Account>()
                        .Fetch(x => x.Transactions)
                        .Fetch(x => x.AccountType)
                        .Fetch(x => x.Customer)
                        .ToList();

                    txn.Commit();
                }
            }
            return accounts;
        }


        public Account GetByAccountNumber(string accNo)
        {
            Account account = new Account();
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    account = session.Query<Account>().Where(a => a.AccountNo == accNo).Fetch(x => x.Transactions)
                        .Fetch(x => x.AccountType).Fetch(x => x.Customer)
                        .Where(x => x.Status == true).FirstOrDefault();
                    txn.Commit();

                }
            }
            return account;
        }


    }
}