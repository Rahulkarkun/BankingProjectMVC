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
           
        

            accountVM.AccountNo= str_build.ToString();
            var account = _accountAssembler.ConvertToModel(accountVM);
            var newUser = _accountService.Add(account);
            ViewBag.Message = "Added Successfully";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var accountData = _accountService.GetById(id);
            var accountDataVM = _accountAssembler.ConvertToViewModel(accountData);
            return View(accountDataVM);
        }
        [HttpPost]
        public ActionResult Edit(AccountVM userVM)
        {
            var account = _accountService.GetById(userVM.Id);
            if (account != null)
            {
                var updatedData = _accountAssembler.ConvertToModel(userVM);
                _accountService.Update(updatedData);
            }
            return RedirectToAction("Index");
        }
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
    }
}
