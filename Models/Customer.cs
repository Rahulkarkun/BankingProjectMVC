using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Models
{
    public class Customer
    {
        public virtual int Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual long ContactNo { get; set; }

        public virtual string Email { get; set; }

        public virtual User User { get; set; }

        public virtual bool Status { get; set; }

        public virtual ISet<Document> Documents { get; set; }

        public virtual ISet<Account> Accounts { get; set; }

        public Customer()
        {
            Documents = new HashSet<Document>();
            Accounts = new HashSet<Account>();
        }


    }
}