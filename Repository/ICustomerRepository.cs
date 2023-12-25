using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProjectMVC.Repository
{
    public interface ICustomerRepository
    {
        string Add(Customer customer);
        string Update(Customer customer);
        string Delete(Customer customer);
        Customer GetById(int custId);
        List<Customer> GetAll();
    }
}
