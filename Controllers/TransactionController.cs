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
        private readonly TransactionAssembler _transactionAssembler;
        public TransactionController(ITransactionService transactionService, TransactionAssembler transactionAssembler)
        {
            _transactionService = transactionService;
            _transactionAssembler = transactionAssembler;
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
            var transaction = _transactionAssembler.ConvertToModel(transactionVM);
            var newTransaction = _transactionService.Add(transaction);
            ViewBag.Message = "Added Successfully";
            return View();
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
            //using (var session = NhibernateHelper.OpenSession())
            //{
            //    using (var txn = session.BeginTransaction())
            //    {
            //var customers = session.Query<Transaction>().ToList();
            var customers = _transactionService.GetAll();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                int searchId;
                if (int.TryParse(searchString, out searchId))
                {
                    // If the search term is a valid integer, search by Id
                    customers = customers.Where(e => e.Id == searchId || e.TransactionType.Contains(searchString)).ToList();
                }
                else
                {
                    // If the search term is not an integer, search by FirstName or LastName
                    customers = customers.Where(e => e.TransactionType.Contains(searchString)).ToList();
                }
            }
            // Get total count of records (for pagination)
            int totalCount = customers.Count();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalCount / rows);

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalCount,
                rows = (from customer in customers
                        orderby sidx + " " + sord
                        select new
                        {
                            cell = new string[] {
                                        customer.Id.ToString(),
                                        customer.TransactionType,
                                        customer.Amount.ToString(),
                                        customer.Date.ToString(),
                                        customer.FromAccountNumber,
                                        customer.ToAccountNumber,
                                    }
                        }).Skip((page - 1) * rows).Take(rows).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
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
    }
}