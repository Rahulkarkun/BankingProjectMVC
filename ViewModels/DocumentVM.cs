using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.ViewModels
{
    public class DocumentVM
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [StringLength(20, ErrorMessage = "Document Name must be at most 20 characters long.")]
        public virtual string DocumentName { get; set; }

        //[Required(ErrorMessage = "Document File is required.")]
        public virtual byte[] DocumentFile { get; set; }

        [Required(ErrorMessage = "Customer Id is required.")]
        public virtual int CustomerId { get; set; }
        public virtual string CustomerName { get; set; }
        public bool IsVerified { get; set; }
        public virtual HttpPostedFileBase PostedFile { get; set; }
    }
}