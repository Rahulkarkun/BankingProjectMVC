using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingProjectMVC.Controllers
{
    [AllowAnonymous]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly TransactionAssembler _transactionAssembler;
        public TransactionController(ITransactionService transactionService, TransactionAssembler transactionAssembler, IAccountService accountService)
        {
            _transactionService = transactionService;
            _transactionAssembler = transactionAssembler;
            _accountService = accountService;
        }
        public ActionResult Index()
        {
            var transactions = _transactionService.GetAll();
            var transactionVMs = transactions.Select(c => _transactionAssembler.ConvertToViewModel(c)).ToList();
            return View(transactionVMs);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TransactionVM transactionVM)
        {
            if (ModelState.IsValid)
            {
                var transaction = _transactionAssembler.ConvertToModel(transactionVM);
                var newTransaction = _transactionService.Add(transaction);
                ViewBag.Message = "Added Successfully";
                return View();
            }
            return View(transactionVM);
        }
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var transactionData = _transactionService.GetById(id);
        //    var transactionDataVM = _transactionAssembler.ConvertToViewModel(transactionData);
        //    return View(transactionDataVM);
        //}
        //[HttpPost]
        //public ActionResult Edit(TransactionVM transactionVM)
        //{
        //    var transaction = _transactionService.GetById(transactionVM.Id);
        //    if (transaction != null)
        //    {
        //        _transactionService.Update(transaction);
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult Edit(TransactionVM transactionVM)
        {
            var transaction = _transactionService.GetById(transactionVM.Id);
            if (transaction != null)
            {
                transaction.Amount = transactionVM.Amount;
                transaction.TransactionType = transactionVM.TransactionType;
                transaction.Date = transactionVM.Date;
                transaction.FromAccountNumber = transactionVM.FromAccountNumber;
                transaction.ToAccountNumber = transactionVM.ToAccountNumber;

                //var updatedData=_transactionAssembler.ConvertToModel(transactionVM);
                _transactionService.Update(transaction);
                return Json(new { success = true, message = "User updated successfully." });
            }
            return Json(new { success = false, message = "No such User Exists" });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var transactionData = _transactionService.GetById(id);
            var transactionDataVM = _transactionAssembler.ConvertToViewModel(transactionData);
            return View(transactionDataVM);
        }
        [HttpPost]
        public ActionResult Delete(TransactionVM transactionVM)
        {
            var transaction = _transactionService.GetById(transactionVM.Id);
            if (transaction != null)
            {
                _transactionService.Delete(transaction);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetData(int page, int rows, string sidx, string sord, string searchString)
        {
            var transactions = _transactionService.GetAll();

            // Check if the user is in the "Admin" role
            if (User.IsInRole("Admin"))
            {
                // Apply search filter if searchString is not empty
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    int searchId;
                    if (int.TryParse(searchString, out searchId))
                    {
                        // If the search term is a valid integer, search by Id
                        transactions = transactions
                            .Where(e => e.Id == searchId)
                            .ToList();
                    }
                    else
                    {
                        // If the search term is not an integer, search by other fields
                        transactions = transactions
                            .Where(e => e.TransactionType.Contains(searchString) ||
                                        e.Amount.ToString().Contains(searchString) ||
                                        e.Date.ToString().Contains(searchString) ||
                                        e.ToAccountNumber.Contains(searchString) ||
                                        e.FromAccountNumber.Contains(searchString))
                            .ToList();
                    }
                }

                // Get total count of records (for pagination)
                int totalCount = transactions.Count();

                // Calculate total pages
                int totalPages = (int)Math.Ceiling((double)totalCount / rows);

                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalCount,
                    rows = (from transaction in transactions
                            orderby sidx + " " + sord
                            select new
                            {
                                cell = new string[]
                                {
                            transaction.Id.ToString(),
                            transaction.TransactionType,
                            transaction.Amount.ToString(),
                            transaction.Date.ToString(),
                            transaction.ToAccountNumber,
                            transaction.FromAccountNumber,
                                }
                            }).Skip((page - 1) * rows).Take(rows).ToArray()
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else if (User.IsInRole("Customer"))
            {
                int customerID;
                if (Session["LoginId"] != null && int.TryParse(Session["LoginId"].ToString(), out customerID))
                {
                    var customerTransactions = _transactionService.GetAll().Where(x => x.Account.Customer.Id == customerID).ToList();

                    if (!string.IsNullOrWhiteSpace(searchString))
                    {
                        int searchId;
                        if (int.TryParse(searchString, out searchId))
                        {
                            customerTransactions = customerTransactions.Where(e => e.Id == searchId).ToList();
                        }
                        else
                        {
                            customerTransactions = customerTransactions.Where(e =>
                                e.TransactionType.Contains(searchString) ||
                                e.Amount.ToString().Contains(searchString) ||
                                e.Date.ToString().Contains(searchString) ||
                                e.ToAccountNumber.Contains(searchString) ||
                                e.FromAccountNumber.Contains(searchString)
                            ).ToList();
                        }
                    }

                    int totalCount = customerTransactions.Count();
                    int totalPages = (int)Math.Ceiling((double)totalCount / rows);

                    var jsonData = new
                    {
                        total = totalPages,
                        page,
                        records = totalCount,
                        rows = (from transaction in customerTransactions
                                orderby sidx + " " + sord
                                select new
                                {
                                    cell = new string[]
                                    {
                                transaction.Id.ToString(),
                                transaction.TransactionType,
                                transaction.Amount.ToString(),
                                transaction.Date.ToString(),
                                transaction.ToAccountNumber,
                                transaction.FromAccountNumber,
                                    }
                                }).Skip((page - 1) * rows).Take(rows).ToArray()
                    };

                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Unauthorized access if the session does not contain a valid customer ID
                    return Json(new { error = "Unauthorized access" });
                }
            }

            // Default return statement or throw an exception
            return Json(new { error = "Invalid role" });
        }


        public ActionResult DownloadExcel()
        {
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Transactions");

            // Add headers
            var headers = new List<string> { "AccountId","TransactionType", "Amount", "Date", "ToAccountNumber", "FromAccountNumber", };
            for (var i = 1; i <= headers.Count; i++)
            {
                worksheet.Cells[1, i].Value = headers[i - 1];
            }

            // Add data
            var rowIndex = 2;
            var transactions = _transactionService.GetAll();
            foreach (var transaction in transactions)
            {
                worksheet.Cells[rowIndex, 1].Value = transaction.Account.Id;
                worksheet.Cells[rowIndex, 2].Value = transaction.TransactionType;
                worksheet.Cells[rowIndex, 3].Value = transaction.Amount;
                worksheet.Cells[rowIndex, 4].Value = transaction.Date.ToString();
                worksheet.Cells[rowIndex, 5].Value = transaction.ToAccountNumber;
                worksheet.Cells[rowIndex, 6].Value = transaction.FromAccountNumber;
                

                rowIndex++;
            }

            // Return the Excel file
            var memoryStream = new MemoryStream(package.GetAsByteArray());
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Transactions.xlsx");
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(TransactionVM transactionVM)
        {
            // Check if FromAccountNumber is null, and set it to "SELF" if it is
            if (string.IsNullOrEmpty(transactionVM.FromAccountNumber))
            {
                transactionVM.FromAccountNumber = "SELF";
            }

            var account = _accountService.GetById(transactionVM.AccountId);
            if (account != null)
            {
                Session["AccountId"] = account.Id;
                account.Balance = account.Balance + transactionVM.Amount;
                _accountService.Update(account);
                var transaction = _transactionAssembler.ConvertToModel(transactionVM);
                var newTransaction = _transactionService.Add(transaction);
                return Json(new { success = true, message = "Amount Deposited Successfully." });
            }
            return Json(new { success = false, message = "No such Account Found." });
        }


        [HttpGet]
        public ActionResult Withdraw()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Withdraw(TransactionVM transactionVM)
        {
            if (string.IsNullOrEmpty(transactionVM.ToAccountNumber))
            {
                transactionVM.ToAccountNumber = "SELF";
            }
            var account = _accountService.GetById(transactionVM.AccountId);
            if (account != null)
            {
                if (account.Balance > transactionVM.Amount)
                {
                    account.Balance = account.Balance - transactionVM.Amount;
                    _accountService.Update(account);
                    var transaction = _transactionAssembler.ConvertToModel(transactionVM);
                    var newTransaction = _transactionService.Add(transaction);
                    return Json(new { success = true, message = "Amount Withdrawn Successfully." });
                }
                return Json(new { success = false, message = "Insuccificent Balance." });
            }
            return Json(new { success = false, message = "No such Account Found." });

        }

    }
}