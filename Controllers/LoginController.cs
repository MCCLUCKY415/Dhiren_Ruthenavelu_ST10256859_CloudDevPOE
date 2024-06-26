﻿using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;

namespace ST10256859_CLDV6211_POE.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string name, string surname)
        {
            var loginModel = new LoginModel();
            int UserID = loginModel.SelectUser(email, password);
            if (UserID != -1)
            {
                HttpContext.Session.SetInt32("UserID", UserID);

                TempData["Name"] = name;
                TempData["Surname"] = surname;

                return RedirectToAction("MyWork", "Product");
            }
            else
            {
                var message = TempData["Message"] as string;
            
                TempData["Message"] = "Incorrect Email or Password. Please try again.";
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}