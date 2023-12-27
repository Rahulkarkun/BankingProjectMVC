using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BankingProjectMVC.Exceptions
{
    public class EntityInsertError : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public EntityInsertError(string message) : base(message)
        {
            StatusCode = (int)HttpStatusCode.NoContent;
            Message = message;
        }
    }
}