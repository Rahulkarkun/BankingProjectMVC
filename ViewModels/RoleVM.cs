using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.ViewModels
{
    public class RoleVM
    {
        [Required(ErrorMessage = "RoleId is required")] 
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "RoleName is required")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "RoleName should contain min 4 and max 10 alphabets")]
        public virtual string RoleName { get; set; }
        public virtual int UsersCount { get; set; }

    }
}