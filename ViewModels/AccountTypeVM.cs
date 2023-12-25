using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.ViewModels
{
    public class AccountTypeVM
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Account Type is required.")]
        //[StringLength(10, ErrorMessage = "Account Type should contain min 4 and max 10 alphabets", MinimumLength = 4)]
        public virtual string Type { get; set; }
        public virtual int AccountsCount { get; set; }
    };
}