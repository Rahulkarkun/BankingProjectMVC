using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BankingProjectMVC.Exceptions
{
    public class EntityNotFoundError : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public EntityNotFoundError(string message) : base(message)
        {
            StatusCode = (int)HttpStatusCode.NotFound;
            Message = message;
        }
    }
}