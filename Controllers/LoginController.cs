﻿using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Exceptions;
using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankingProjectMVC.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        //private MyContext _myContext = new MyContext();
        // GET: Login
        private readonly IUserService _userService;
        private readonly UserAssembler _userAssembler;
        private readonly ICustomerService _customerService;

        public LoginController(IUserService userService, UserAssembler userAssembler, ICustomerService customerService)
        {
            _userService = userService;
            _userAssembler = userAssembler;
            _customerService = customerService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserVM userVM)
        {
            try
            {
                
                var user = _userService.GetUserByUsername(userVM.Username);
                //var customers = _customerService.GetAll();
                //var data = customers.Where(x => x.User.Id == user.Id).FirstOrDefault();
                //Session["LoginId"] = data.Id;
                //TempData["LoginId"] = data.Id;
                User result = null;
                var getUser = _userService.GetUserWithRole(user.Id);

                if (userVM.Username == getUser.Username && VerifyPassword(userVM.Password,getUser.Password))
                {
                    result = getUser;
                }

                if (result != null)
                {
                    Session["User"] = result.Username;
                    //Session["Role"] = result.Role.RoleName;
                    Session["UserId"] = result.Id;
                    var customers = _customerService.GetAll();
                    var data = customers.Where(x => x.User.Id == user.Id).FirstOrDefault();
                    Session["LoginId"] = data.Id;
                    FormsAuthentication.SetAuthCookie(result.Username, false);

                    if (result.Role.RoleName == "Admin")
                        return RedirectToAction("AdminDashboard", "Customer");

                    return RedirectToAction("CustomerDashboard", "Customer");
                }

                ViewBag.Message = "Username or Password does not match";

                return View();
            }
            catch (EntityNotFoundError ex)
            {
                // Log the exception or handle it as needed
                ViewBag.Message = $"Error: {ex.Message}";
                return View(); // or return an appropriate error view
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                ViewBag.Message = $"An unexpected error occurred: {ex.Message}";
                return View(); // or return an appropriate error view
            }
        }
        public ActionResult Logout()
        {
            //Session.Clear();
            FormsAuthentication.SignOut();
            Session["LoginId"] = null; // Clear the customer's ID from the session
            return RedirectToAction("Login");
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // ComputeHash - returns byte array, convert it to a string
                byte[] enteredPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < enteredPasswordBytes.Length; i++)
                {
                    builder.Append(enteredPasswordBytes[i].ToString("x2"));
                }

                return builder.ToString() == hashedPassword;
            }
        }
    }
}