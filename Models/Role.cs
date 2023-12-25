using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string RoleName { get; set; }
        public virtual ISet<User> Users { get; set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}