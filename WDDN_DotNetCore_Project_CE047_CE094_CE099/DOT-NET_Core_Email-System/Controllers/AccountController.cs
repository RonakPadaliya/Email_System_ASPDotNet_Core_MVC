using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOT_NET_Core_Email_System.Models;
using DOT_NET_Core_Email_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DOT_NET_Core_Email_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly string UserEmailSession = "_UserEmail";
        private readonly string UserNameSession = "_UserName";
        private readonly IUserRepo userRepo;
        public AccountController(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public IActionResult SignUp()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost, ActionName("SignUp")]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                DbUser temp=userRepo.GetUser(model.UserName);

                if(temp!=null)
                {
                    ModelState.AddModelError(string.Empty,"User is all ready exist !");
                    return View();
                }
                var dbuser = new DbUser
                {
                    UserName = model.UserName,
                    UserFirstName = model.UserFirstName,
                    UserLastName = model.UserLastName,
                    UserPass = model.Password,
                    DOB = model.DOB,
                    UserEmailId = model.UserName + "@email.com",
                };
                DbUser newUser = userRepo.Add(dbuser);
                HttpContext.Session.SetString(UserNameSession, newUser.UserName);
                HttpContext.Session.SetString(UserEmailSession, newUser.UserEmailId);
                return RedirectToAction("inbox", "sidebar");
            }
            return View();
        }
        
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                DbUser dbuser = userRepo.GetUserEmail(model.Email);
                if (dbuser != null)
                {
                    if (dbuser.UserPass == model.Password)
                    {
                        HttpContext.Session.SetString(UserNameSession, dbuser.UserName);
                        HttpContext.Session.SetString(UserEmailSession, dbuser.UserEmailId);
                        return RedirectToAction("inbox", "sidebar");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Password");
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No user found,\n Please Sign up yourself");
                    return View();
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "home");
        }
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString(UserNameSession) == null || HttpContext.Session.GetString(UserEmailSession) == null)
            {
                Response.Redirect("/Account/Login");
            }
            var model = userRepo.GetUserEmail(HttpContext.Session.GetString(UserEmailSession));
            return View(model);
        }
        public IActionResult UpdateProfile()
        {
            if (HttpContext.Session.GetString(UserNameSession) == null || HttpContext.Session.GetString(UserEmailSession) == null)
            {
                Response.Redirect("/Account/Login");
            }
            DbUser user = userRepo.GetUserEmail(HttpContext.Session.GetString(UserEmailSession));
            UpdateProfileViewModel model = new UpdateProfileViewModel
            {
                UserName = user.UserName,
                NewPassword = "",
                ConfirmPassword = "",
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProfile(UpdateProfileViewModel model)
        {
            if (HttpContext.Session.GetString(UserNameSession) == null || HttpContext.Session.GetString(UserEmailSession) == null)
            {
                Response.Redirect("/Account/Login");
            }
            if (ModelState.IsValid)
            {
                DbUser user = userRepo.GetUserEmail(HttpContext.Session.GetString("_UserEmail"));
                user.UserPass = model.NewPassword;
                DbUser updateUser = userRepo.Update(user);
                return RedirectToAction("profile", "account");

            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

