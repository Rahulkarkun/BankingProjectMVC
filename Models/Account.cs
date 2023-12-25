using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace BankingProjectMVC.Models
{
    public class Account
    {
        public virtual int Id { get; set; }
        public virtual string AccountNo { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ISet<Transaction> Transactions { get; set; }
        public virtual bool Status { get; set; }
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

    }
}