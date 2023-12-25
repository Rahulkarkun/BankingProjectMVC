using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using BankingProjectMVC.Repository;
using BankingProjectMVC.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingAppMVC.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public string Add(Customer customer)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(customer);
                    txn.Commit();

                }
            }
            return "Added Succesfully";
        }
        public string Update(Customer customer)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(customer);
                    txn.Commit();

                }
            }
            return "Updated Succesfully";
        }
        public string Delete(Customer customer)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    customer.Status = false;
                    session.Update(customer);
                    //session.Delete(customer);
                    txn.Commit();

                }
            }
            return "Deleted Succesfully";
        }
        //public Customer GetById(int custId)
        //{
        //    Customer customer = null;
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        using (var txn = session.BeginTransaction())
        //        {
        //            customer = session.Load<Customer>(custId);
        //            txn.Commit();

        //        }
        //    }
        //    return customer;
        //}

        //public Customer GetById(int custId)
        //{
        //    Customer customer = null;
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        using (var txn = session.BeginTransaction())
        //        {
        //            customer = session.Query<Customer>()
        //                .Fetch(c => c.Documents)
        //                .Fetch(c => c.Accounts)
        //                .SingleOrDefault(c => c.Id == custId);

        //            txn.Commit();
        //        }
        //    }
        //    return customer;
        //}

        public Customer GetById(int custId)
        {
            Customer customer = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    customer = session.Query<Customer>().Where(c => c.Id == custId)
                        .Fetch(c => c.Documents)
                        .Fetch(c => c.Accounts)
                         .Fetch(c => c.User)
                        .FirstOrDefault();
                    txn.Commit();
                }
            }
            return customer;
        }

        


        public List<Customer> GetAll()
        {
            //List<Customer> customer = new List<Customer>();
            List<Customer> customer = new List<Customer>() { };
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    //    customer = session.Query<Customer>()
                    //.Fetch(c => c.Documents)
                    //.Fetch(c => c.Accounts)
                    //.ToList();

                    customer = session.Query<Customer>()
                    .Fetch(d => d.Documents)
                    .Fetch(d => d.Accounts)
                    .Fetch(d => d.User)
                    .Where(d => d.Status == true)
                    .ToList();

                    //txn.Commit();
                    txn.Commit();

                }
            }
            return customer;
        }
    }
}