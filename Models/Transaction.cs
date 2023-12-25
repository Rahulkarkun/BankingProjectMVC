using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Models
{
    public class Transaction
    {
        public virtual int Id { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string ToAccountNumber { get; set; }
        public virtual string FromAccountNumber { get; set; }
        public virtual Account Account { get; set; }

    }
}