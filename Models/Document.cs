using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Models
{
    public class Document
    {
        
            public virtual int Id { get; set; }

            public virtual string DocumentName { get; set; }

            public virtual byte[] DocumentFile { get; set; }

            public virtual Customer Customer { get; set; }

    }
}