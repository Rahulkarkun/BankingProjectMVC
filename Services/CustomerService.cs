using BankingProjectMVC.Models;
using BankingProjectMVC.Repository;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public string Add(Customer customer)
        {
            return _customerRepository.Add(customer);
        }
        public string Update(Customer customer)
        {
            return _customerRepository.Update(customer);
        }
        public string Delete(Customer customer)
        {
            return _customerRepository.Delete(customer);
        }
        //public Customer GetById(int customerId)
        //{
        //    return _customerRepository.GetById(customerId);
        //}

        public Customer GetById(int custId)
        {
            var customer = _customerRepository.GetById(custId);
            // Explicitly fetch collections
            //NHibernateUtil.Initialize(customer.Documents);
            //NHibernateUtil.Initialize(customer.Accounts);
            return customer;
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }
    }
}