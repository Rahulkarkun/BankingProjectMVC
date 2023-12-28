using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BankingProjectMVC.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserService _userService; 
        private readonly UserAssembler _userAssembler;
        public UserController(IUserService userService, UserAssembler userAssembler)
        {
            _userService = userService; _userAssembler = userAssembler;
        }
        public ActionResult Index()
        {
            var users = _userService.GetAll();
            List<UserVM> list = new List<UserVM>();
            foreach (var user in users)
            {
                list.Add(_userAssembler.ConvertToViewModel(user));
            }

            // Implement logic for displaying account types if needed
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = _userAssembler.ConvertToModel(userVM);
                var newUser = _userService.Add(user);
                ViewBag.Message = "Added Successfully";
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, return the view with validation errors
            return View(userVM);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userData = _userService.GetById(id);
            var userDataVM = _userAssembler.ConvertToViewModel(userData); return View(userDataVM);
        }
        [HttpPost]
        public ActionResult Edit(UserVM userVM)
        {
            var user = _userService.GetById(userVM.Id); if (user != null)
            {
                _userService.Update(user);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var userData = _userService.GetById(id); var userDataVM = _userAssembler.ConvertToViewModel(userData);
            return View(userDataVM);
        }
        [HttpPost]
        public ActionResult Delete(UserVM userVM)
        {
            var user = _userService.GetById(userVM.Id);
            if (user != null)
            {
                _userService.Delete(user);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            //var userData = _userService.GetById(id);
            //var userDataVM = _userAssembler.ConvertToViewModel(userData);
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UserVM userVM, string confirmNewPassword)
        {
            ModelState.Remove("Username");
            ModelState.Remove("RoleId");
            if (ModelState.IsValid)
            {
                if (userVM.Password != confirmNewPassword)
                {
                    return Json(new { success = false, message = "New Password and Confirm Password not matching." });
                }

                userVM.Password = HashPassword(confirmNewPassword);

                // Ensure that Session["UserId"] is set correctly
                if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out int userId))
                {
                    userVM.Id = userId;

                    // Get the user from the service
                    var user = _userService.GetById(userId);

                    if (user != null)
                    {
                        // Update the user's password
                        user.Password = userVM.Password;

                        // Ensure that your Update method persists changes to the database
                        _userService.Update(user);

                        return Json(new { success = true, message = "Password Changed Successfully." });
                    }
                }

                return Json(new { success = false, message = "User not found or Session ID is not set." });
            }
            return View();
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

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // ComputeHash - returns byte array, convert it to a string
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
