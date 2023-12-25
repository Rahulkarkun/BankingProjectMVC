using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingProjectMVC.Controllers
{
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly AccountTypeAssembler _accountTypeAssembler;

        public AccountTypeController(IAccountTypeService accountTypeService, AccountTypeAssembler accountTypeAssembler)
        {
            _accountTypeService = accountTypeService;
            _accountTypeAssembler = accountTypeAssembler;
        }

        public ActionResult Index()
        {
            // Implement logic for displaying account types if needed
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AccountTypeVM accountTypeVM)
        {
            var accountType = _accountTypeAssembler.ConvertToModel(accountTypeVM);
            var newAccountType = _accountTypeService.Add(accountType);
            ViewBag.Message = "Added Successfully";
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var accountTypeData = _accountTypeService.GetById(id);
            var accountTypeDataVM = _accountTypeAssembler.ConvertToViewModel(accountTypeData);
            return View(accountTypeDataVM);
        }

        [HttpPost]
        public ActionResult Edit(AccountTypeVM accountTypeVM)
        {
            var accountType = _accountTypeService.GetById(accountTypeVM.Id);
            if (accountType != null)
            {
                _accountTypeService.Update(accountType);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var accountTypeData = _accountTypeService.GetById(id);
            var accountTypeDataVM = _accountTypeAssembler.ConvertToViewModel(accountTypeData);
            return View(accountTypeDataVM);
        }

        [HttpPost]
        public ActionResult Delete(AccountTypeVM accountTypeVM)
        {
            var accountType = _accountTypeService.GetById(accountTypeVM.Id);
            if (accountType != null)
            {
                _accountTypeService.Delete(accountType);
            }
            return RedirectToAction("Index");
        }
    }
}