using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Models
{
    public class AccountType
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual ISet<Account> Accounts { get; set; }
        public AccountType()
        {
            Accounts = new HashSet<Account>();
        }
    }
}