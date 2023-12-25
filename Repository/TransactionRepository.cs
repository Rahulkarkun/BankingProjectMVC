using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public string Add(Transaction transaction)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(transaction);
                    txn.Commit();

                }
            }
            return "Added Succesfully";
        }
        public string Update(Transaction transaction)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(transaction);
                    txn.Commit();

                }
            }
            return "Updated Succesfully";
        }
        public string Delete(Transaction transaction)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Delete(transaction);
                    txn.Commit();

                }
            }
            return "Deleted Succesfully";
        }
        public Transaction GetById(int transactionId)
        {
            Transaction transaction = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    transaction = session.Query<Transaction>().Where(x => x.Id == transactionId)
                        .Fetch(x => x.Account).FirstOrDefault();
                    txn.Commit();


                }
            }
            return transaction;
        }
        //public List<Transaction> GetAll()
        //{
        //    List<Transaction> transaction = new List<Transaction>() { };
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        using (var txn = session.BeginTransaction())
        //        {
        //            transaction = session.Query<Transaction>().ToList();
        //            txn.Commit();

        //        }
        //    }
        //    return transaction;
        //}
        public List<Transaction> GetAll()
        {
            List<Transaction> transaction = new List<Transaction>();
            //List<Customer> customer = new List<Customer>() { };
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    //    customer = session.Query<Customer>()
                    //.Fetch(c => c.Documents)
                    //.Fetch(c => c.Accounts)
                    //.ToList();

                    transaction = session.Query<Transaction>()
                    //.Fetch(c => c.User)
                    .Fetch(c => c.Account)
                    .ToList();

                    //txn.Commit();
                    txn.Commit();

                }
            }
            return transaction;
        }
    }
}