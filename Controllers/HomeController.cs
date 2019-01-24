using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Csharp_LognReg.Models;


namespace Csharp_LognReg.Controllers
{
    public class HomeController : Controller
    {
        private UserContext dbContext;

        public HomeController(UserContext context)
        {
            dbContext = context;
        }
//-------Registration getter-------
        [HttpGet("")]
        public IActionResult RegisterUser()
        {
            return View("Index");
        }
//--------Registration setter------
        [HttpPost("")]
        public IActionResult RegisterUser(User uv)
        {
            if (uv.Password != uv.Confirm) 
            {
                ModelState.AddModelError("Password", "Confirmation Password must match the Password");
            }
            User userchk = dbContext.User.Where(e=> e.Email == uv.Email).FirstOrDefault();
            if (userchk != null)
            {
                ModelState.AddModelError("Email", "User with this email already exists");
            }
            if (ModelState.IsValid)
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                uv.Password = hasher.HashPassword(uv, uv.Password);
                dbContext.User.Add(uv);
                dbContext.SaveChanges();
                int UserId = dbContext.User.Last().Id;
                HttpContext.Session.SetInt32("UserId", UserId);
                return View("Success");
            }
            return View("Index");
        }
//--------Login getter--------------
        [HttpGet("login")]
        public IActionResult LoginSplash()
        {
            return View("login");
        }
//--------Login setter--------------
        [HttpPost("login")]
        public IActionResult Login(LoginUser userInfo)
        {
            System.Console.WriteLine(userInfo.Email);
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("======================================================");
                User user = dbContext.User.FirstOrDefault(u => u.Email == userInfo.Email);
                if(user == null)
                {
                    ModelState.AddModelError("LoginUser.Email", "Invalid Email/Password");
                    return View("Login");
                }
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, userInfo.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserId", userInfo.Id); //session loggedin
                return RedirectToAction("Success");
            }
            return View("Login");
        }
//----------Success route----------
        [HttpGet("Success")]
        public IActionResult Success()
        {
            return View();
        }
    }
}