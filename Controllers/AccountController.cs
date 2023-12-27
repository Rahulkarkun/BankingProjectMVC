using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BankingProjectMVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        // GET: User
        private readonly IAccountService _accountService;
        private readonly AccountAssembler _accountAssembler;
        public AccountController(IAccountService accountService
        , AccountAssembler accountAssembler)
        {
            _accountService = accountService;
            _accountAssembler = accountAssembler;

        }
        public ActionResult Index()
        {
            var accounts = _accountService.GetAll();
            List<AccountVM> list = new List<AccountVM>();
            foreach (var account in accounts)
            {
                list.Add(_accountAssembler.ConvertToViewModel(account));
            }

            // Implement logic for displaying account types if needed
            return View(list);
            //return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AccountVM accountVM)
        {
            if (ModelState.IsValid)
            {
                int length = 3;

                // creating a StringBuilder object()
                StringBuilder str_build = new StringBuilder();
                Random random = new Random();

                char letter;
                int shift = 0;
                int v = 0;
                for (int i = 0; i < length; i++)
                {
                    double flt = random.NextDouble();
                    v = random.Next(100, 999);
                    shift = Convert.ToInt32(Math.Floor(25 * flt));
                    letter = Convert.ToChar(shift + 65);

                    str_build.Append(letter);
                }
                str_build.Append(v.ToString());



                accountVM.AccountNo = str_build.ToString();
                var account = _accountAssembler.ConvertToModel(accountVM);
                var newUser = _accountService.Add(account);
                ViewBag.Message = "Added Successfully";
                return View();
            }
            return (View(accountVM));
        }
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var accountData = _accountService.GetById(id);
        //    var accountDataVM = _accountAssembler.ConvertToViewModel(accountData);
        //    return View(accountDataVM);
        //}
        //[HttpPost]
        //public ActionResult Edit(AccountVM userVM)
        //{
        //    var account = _accountService.GetById(userVM.Id);
        //    if (account != null)
        //    {
        //        var updatedData = _accountAssembler.ConvertToModel(userVM);
        //        _accountService.Update(updatedData);
        //    }
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var accountData = _accountService.GetById(id);
            var accountDataVM = _accountAssembler.ConvertToViewModel(accountData);
            return View(accountDataVM);
        }
        [HttpPost]
        public ActionResult Delete(AccountVM accountVM)
        {
            var account = _accountService.GetById(accountVM.Id);
            if (account != null)
            {
                _accountService.Delete(account);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetData(int page, int rows, string sidx, string sord, string searchString)
        {
            var accounts = _accountService.GetAll();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                int searchId;
                if (int.TryParse(searchString, out searchId))
                {
                    // If the search term is a valid integer, search by Id
                    accounts = accounts.Where(e => e.Id == searchId).ToList();
                }
                else
                {
                    // If the search term is not an integer, search by other fields
                    accounts = accounts.Where(e =>
                        e.AccountNo.Contains(searchString) ||
                        e.AccountType.Type.ToString().Contains(searchString) ||
                        e.Balance.ToString().Contains(searchString) ||
                        (e.Customer.FirstName + " " + e.Customer.LastName).Contains(searchString) ||
                        e.Transactions.Count().ToString().Contains(searchString) ||
                        e.Status.ToString().Contains(searchString)
                    ).ToList();
                }
            }

            // Get total count of records (for pagination)
            int totalCount = accounts.Count();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalCount / rows);

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalCount,
                rows = (from account in accounts
                        orderby sidx + " " + sord
                        select new
                        {
                            cell = new string[] {
                                account.Id.ToString(),
                                account.AccountNo,
                                account.AccountType.Type,  // Use Type property of AccountType
                                account.Balance.ToString(),
                                $"{account.Customer.FirstName} {account.Customer.LastName}",  // Concatenate FirstName and LastName
                                account.Transactions.Count().ToString(),
                                account.Status.ToString(),
                                //customer.IsActive?"True":"False",
                            }
                        }).Skip((page - 1) * rows).Take(rows).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Edit(Account account)
        {
            //using (var session = NHibernateHelper.OpenSession())
            //{
            //    using (var txn = session.BeginTransaction())
            //    {
            var existingAccount = _accountService.GetById(account.Id);
            if (existingAccount != null)
            {
                existingAccount.AccountNo = account.AccountNo;
                existingAccount.AccountType = account.AccountType;
                existingAccount.Customer = account.Customer;
                existingAccount.Balance = account.Balance;
                existingAccount.Transactions = account.Transactions;
                existingAccount.Status = account.Status;
                _accountService.Update(existingAccount);
                //session.Update(existingCustomer);
                //txn.Commit();
                return Json(new { success = true, message = "User updated successfully." });
            }
            //return RedirectToAction("Index");

            return Json(new { success = false, message = "No such User Exists" });
        }

    }
}
