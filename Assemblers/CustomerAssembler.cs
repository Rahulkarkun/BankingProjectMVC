using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Assemblers
{
    public class CustomerAssembler
    {
        private readonly IUserService _userService;

        public CustomerAssembler(IUserService userService)
        {
            _userService = userService;
        }
        public Customer ConvertToModel(CustomerVM customerVM)
        {
            var User = _userService.GetById(customerVM.UserId);
            return new Customer
            {
                Id = customerVM.Id,
                FirstName = customerVM.FirstName,
                LastName = customerVM.LastName,
                ContactNo = customerVM.ContactNo,
                Email = customerVM.Email,
                Status = customerVM.Status,
                //User = new User() { Id = customerVM.UserId },
                User = User
            };
        }

        public CustomerVM ConvertToViewModel(Customer customer)
        {
            return new CustomerVM
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ContactNo = customer.ContactNo,
                Email = customer.Email,
                Status = customer.Status,
                UserId = customer.User.Id,
                DocumentsCount =  customer.Documents.Count ,
                AccountsCount =  customer.Accounts.Count ,
            };
        }
    }
}