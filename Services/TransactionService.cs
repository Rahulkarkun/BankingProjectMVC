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
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        public TransactionService(ITransactionRepository transactionRepository, ICustomerService customerService, IAccountService accountService)
        {
            _transactionRepository = transactionRepository;
            _customerService = customerService;
            _accountService = accountService;
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

        public List<Transaction> GetAllByCustFilter(int tempData)
        {
            var accounts = _accountService.GetAll();

            accounts = accounts.Where(a => a.Customer.Id == tempData).ToList();

            var transactions = _transactionRepository.GetAll();
            List<Transaction> filteredTransaction = new List<Transaction>();
            foreach (var account in accounts)
            {
                var transaction = transactions.Where(t => t.Account.Id == account.Id).ToList();
                foreach (var tr in transaction)
                {
                    filteredTransaction.Add(tr);

                }

            }
            return filteredTransaction;


        }
    }
}