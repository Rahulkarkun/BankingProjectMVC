using BankingProjectMVC.Models;
using BankingProjectMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public string Add(Transaction transaction)
        {
            return _transactionRepository.Add(transaction);
        }
        public string Update(Transaction transaction)
        {
            return _transactionRepository.Update(transaction);
        }
        public string Delete(Transaction transaction)
        {
            return _transactionRepository.Delete(transaction);
        }
        public Transaction GetById(int transactionId)
        {
            return _transactionRepository.GetById(transactionId);
        }
        public List<Transaction> GetAll()
        {
            return _transactionRepository.GetAll();
        }
    }
}