using BankingProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Repository
{
    public interface ITransactionRepository
    {
        string Add(Transaction transaction);
        string Update(Transaction transaction);
        string Delete(Transaction transaction);
        Transaction GetById(int transactionId);
        List<Transaction> GetAll();
    }
}