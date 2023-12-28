using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Assemblers
{
    public class TransactionAssembler
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;


        public TransactionAssembler(ITransactionService transactionService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }
        public TransactionVM ConvertToViewModel(Transaction transaction)
        {

            return new TransactionVM()
            {
                Id = transaction.Id,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                Date = transaction.Date,
                ToAccountNumber = transaction.ToAccountNumber,
                FromAccountNumber = transaction.FromAccountNumber,
                AccountId = transaction.Account.Id,

            };
        }

        public Transaction ConvertToModel(TransactionVM transactionVM)
        {
            var acc = _accountService.GetById(transactionVM.AccountId);
            return new Transaction()
            {
                Id = transactionVM.Id,
                TransactionType = transactionVM.TransactionType,
                Amount = transactionVM.Amount,
                Date = transactionVM.Date,
                ToAccountNumber = transactionVM.ToAccountNumber,
                FromAccountNumber = transactionVM.FromAccountNumber,
                //Account = new Account() { Id = transactionVM.AccountId },
                Account = acc,

            };
        }
    }
}