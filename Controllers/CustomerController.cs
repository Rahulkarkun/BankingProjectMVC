
using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Helpers;
using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingAppMVC.Controllers
{
    [AllowAnonymous]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly CustomerAssembler _customerAssembler;
        // GET: Customer
        //public ActionResult Index()
        //{
        //    var customers = _customerService.GetAll();
        //    return View(customers);
        //}

        //public ActionResult Index()
        //{
        //    var customers = _customerService.GetAll().Select(c => _customerAssembler.ConvertToViewModel(c)).ToList();
        //    return View(customers);
        //}

        //public ActionResult Index()
        //{
        //    var customers = _customerService.GetAll().Select(c => _customerAssembler.ConvertToViewModel(c)).ToList();
        //    return View(customers);
        //}

        public ActionResult AdminDashboard()
        {
            // You can add any additional logic or data retrieval here if needed
            return View();
        }

        public ActionResult CustomerDashboard()
        {
            // You can add any additional logic or data retrieval here if needed
            return View();
        }

        public ActionResult Index()
        {
            var customers = _customerService.GetAll();
            var customerVMs = customers.Select(c => _customerAssembler.ConvertToViewModel(c)).ToList();
            return View(customerVMs);
        }


        public CustomerController(ICustomerService customerService, CustomerAssembler customerAssembler)
        {
            _customerService = customerService;
            _customerAssembler = customerAssembler;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerAssembler.ConvertToModel(customerVM);
                var newCustomer = _customerService.Add(customer);
                ViewBag.Message = "Added Successfully";
                return View();
            }
            return View(customerVM);
        }
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var customerData = _customerService.GetById(id);
        //    var customerDataVM = _customerAssembler.ConvertToViewModel(customerData);
        //    return View(customerDataVM);
        //}
        //[HttpPost]
        //public ActionResult Edit(CustomerVM customerVM)
        //{
        //    var customer = _customerService.GetById(customerVM.Id);
        //    if (customer != null)
        //    {
        //        var updatedCustomer = _customerAssembler.ConvertToModel(customerVM);
        //        _customerService.Update(updatedCustomer);
        //    }
        //    return RedirectToAction("Index");
        //}
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    var customerData = _customerService.GetById(id);
        //    var customerDataVM = _customerAssembler.ConvertToViewModel(customerData);
        //    return View(customerDataVM); ;
        //}
        //[HttpPost]
        //public ActionResult Delete(CustomerVM customerVM)
        //{
        //    var customer = _customerService.GetById(customerVM.Id);
        //    if (customer != null)
        //    {
        //        _customerService.Delete(customer);
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public JsonResult GetData(int page, int rows, string sidx, string sord, string searchString)
        {
            var customers = _customerService.GetAll();
            //if (!string.IsNullOrWhiteSpace(searchString))
            //        {
            //            int searchId;
            //            if (int.TryParse(searchString, out searchId))
            //            {
            //                // If the search term is a valid integer, search by Id
            //                customers = customers.Where(e => e.Id == searchId || e.FirstName.Contains(searchString) || e.LastName.Contains(searchString)).ToList();
            //            }
            //            else
            //            {
            //                // If the search term is not an integer, search by FirstName or LastName
            //                customers = customers.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString)).ToList();
            //            }
            //        }

            //if (!string.IsNullOrWhiteSpace(searchString))
            //{
            //    customers = customers.Where(e =>
            //        e.Id.ToString().Contains(searchString) ||
            //        e.FirstName.Contains(searchString) ||
            //        e.LastName.Contains(searchString) ||
            //        e.ContactNo.ToString().Contains(searchString) ||
            //        e.Email.Trim().Equals(searchString.Trim(), StringComparison.OrdinalIgnoreCase) ||
            //        //e.Email.Contains(searchString) ||
            //        e.User.Id.ToString().Contains(searchString) ||
            //        e.Status.ToString().Contains(searchString) ||
            //        e.Accounts.Count.ToString().Contains(searchString) ||
            //        e.Documents.Count.ToString().Contains(searchString)
            //    ).ToList();
            //}

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                int searchId;
                if (int.TryParse(searchString, out searchId))
                {
                    // If the search term is a valid integer, search by Id
                    customers = customers.Where(e => e.Id == searchId).ToList();
                }
                else
                {
                    // If the search term is not an integer, search by other fields
                    customers = customers.Where(e =>
                        e.FirstName.Contains(searchString) ||
                        e.LastName.Contains(searchString) ||
                        e.ContactNo.ToString().Contains(searchString) ||
                        e.Email.Trim().Equals(searchString.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        e.User.Id.ToString().Contains(searchString) ||
                        e.Status.ToString().Contains(searchString) ||
                        e.Accounts.Count.ToString().Contains(searchString) ||
                        e.Documents.Count.ToString().Contains(searchString)
                    ).ToList();
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
                                        customer.FirstName,
                                        customer.LastName,
                                        customer.ContactNo.ToString(),
                                        customer.Email,
                                        customer.User.Id.ToString(),
                                        customer.Status.ToString(),
                                        customer.Accounts.Count.ToString(),
                                        customer.Documents.Count.ToString(),
                                        //customer.IsActive?"True":"False",
                                    }
                                }).Skip((page - 1) * rows).Take(rows).ToArray()
                    };
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                //}
            //}

        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            //using (var session = NHibernateHelper.OpenSession())
            //{
            //    using (var txn = session.BeginTransaction())
            //    {
                    var existingCustomer = _customerService.GetById(customer.Id);
                    if (existingCustomer != null)
                    {
                        existingCustomer.FirstName = customer.FirstName;
                        existingCustomer.LastName = customer.LastName;
                        existingCustomer.Email = customer.Email;
                        existingCustomer.Accounts = customer.Accounts;
                        _customerService.Update(existingCustomer);
                //session.Update(existingCustomer);
                //txn.Commit();
                return Json(new { success = true, message = "User updated successfully." });
            }
            //return RedirectToAction("Index");

            return Json(new { success = false, message = "No such User Exists" });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //using (var session = NHibernateHelper.OpenSession())
            //{
            //    using (var txn = session.BeginTransaction())
            //    {
                    var existingCustomer = _customerService.GetById(id);
                    if (existingCustomer != null)
                    {
                    //existingCustomer.Status = false;
                    //session.Update(existingCustomer);

                    //hard delete
                    _customerService.Delete(existingCustomer);
                        //txn.Commit();
                        return Json(new { success = true, message = "User deleted successfully." });
                    }
                    return Json(new { success = false, message = "No such User Exists" });
                //}
            //}
        }
    }

    

}