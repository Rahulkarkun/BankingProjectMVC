using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.ViewModels
{
    public class CustomerVM
    {
            public virtual int Id { get; set; }
            [Required(ErrorMessage = "FirstName is required.")]
            [StringLength(20, MinimumLength = 4, ErrorMessage = "FirstName should contain min 4 and max 10 alphabets")]
            public virtual string FirstName { get; set; }
            [Required(ErrorMessage = "LastName is required.")]
            [StringLength(20, MinimumLength = 4, ErrorMessage = "LastName should contain min 4 and max 10 alphabets")]
            public virtual string LastName { get; set; }
            [Required(ErrorMessage = "Contact number is required.")]
            [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid mobile number format.")]
            public virtual long ContactNo { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public virtual string Email { get; set; }
            [Required(ErrorMessage = "UserId is required")]
            public virtual int UserId { get; set; }
            public virtual bool Status { get; set; }
            public virtual int AccountsCount { get; set; }
            public virtual int DocumentsCount { get; set; }


    }
}