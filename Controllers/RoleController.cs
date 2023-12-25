using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;

namespace BankingProjectMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly RoleAssembler _roleAssembler;
        public RoleController(IRoleService roleService, RoleAssembler roleAssembler)
        {
            _roleService = roleService;
            _roleAssembler = roleAssembler;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            var role = _roleAssembler.ConvertToModel(roleVM);
            var newRole = _roleService.Add(role);
            ViewBag.Message = "Added Successfully";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var roleData = _roleService.GetById(id);
            var roleDataVM = _roleAssembler.ConvertToViewModel(roleData);
            return View(roleDataVM);
        }
        [HttpPost]
        public ActionResult Edit(RoleVM roleVM)
        {
            var role = _roleService.GetById(roleVM.Id);
            if (role != null)
            {
                _roleService.Update(role);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var roleData = _roleService.GetById(id);
            var roleDataVM = _roleAssembler.ConvertToViewModel(roleData);
            return View(roleDataVM);
        }
        [HttpPost]
        public ActionResult Delete(RoleVM roleVM)
        {
            var role = _roleService.GetById(roleVM.Id);
            if (role != null)
            {
                _roleService.Delete(role);
            }
            return RedirectToAction("Index");
        }
    }
}