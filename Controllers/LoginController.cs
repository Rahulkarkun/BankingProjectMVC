using BankingProjectMVC.Assemblers;
using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LoginController(IUserService userService, UserAssembler userAssembler)
        {
            _userService = userService;
            _userAssembler = userAssembler;
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
            var user = _userService.GetUserByUsername(userVM.Username);
            //var result1 = _myContext.Users.Where(x => x.Name == user.Name && x.Password == user.Password).Include(x => x.Role).FirstOrDefault();
            User result = null;
            var getUser = _userService.GetUserWithRole(user.Id);
            if (userVM.Username == getUser.Username && userVM.Password == getUser.Password)
            {
                result = getUser;
            }
            if (result != null)
            {
                //Session["User"] = result.Name;
                //Session["Role"] = result.Role.RoleName;
                FormsAuthentication.SetAuthCookie(result.Username, false);
                if (result.Role.RoleName == "Admin")
                    return RedirectToAction("Index", "User");
                return RedirectToAction("Index", "Customer");
            }

            ViewBag.Message = "Username or Password does not match";

            return View();
        }
        public ActionResult Logout()
        {
            //Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}