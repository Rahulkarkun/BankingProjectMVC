using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.ViewModels
{
    public class AccountVM
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Account Number is required.")]
        public virtual string AccountNo { get; set; }
        [Required(ErrorMessage = "Account Type Id is required.")]
        public virtual int AccountTypeId { get; set; }
        [Required(ErrorMessage = "Balance is required.")]
        public virtual decimal Balance { get; set; }
        [Required(ErrorMessage = "CustomerId is required.")]
        public virtual int CustomerId { get; set; }
        public virtual int TransactionsCount { get; set; }
        public virtual bool Status { get; set; }
    }
}