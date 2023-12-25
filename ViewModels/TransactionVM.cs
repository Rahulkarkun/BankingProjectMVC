using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.ViewModels
{
    public class TransactionVM
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Transaction Type is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Transaction Type should contain min 4 and max 10 alphabets")]
        public virtual string TransactionType { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public virtual decimal Amount { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        public virtual DateTime Date { get; set; }
        [Required(ErrorMessage = "To Account Number is required.")]
        public virtual string ToAccountNumber { get; set; }
        [Required(ErrorMessage = "From Account Number is required.")]
        public virtual string FromAccountNumber { get; set; }

        [Required(ErrorMessage = "From Account Id is required.")]
        public virtual int AccountId { get; set; }


    }
}