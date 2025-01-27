﻿using FundRaiser_Team1_MVC.Models;
using FundRaiser_Team1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Data;
using System.Web;

namespace FunderRaiser_Team1_Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WelcomePage()
        {
            return View();
        }


        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            //Delete Cookies
            if (Request.Cookies["userId"] != null)
            {
                Response.Cookies.Delete("userId");
            }
            if (Request.Cookies["userCategory"] != null)
            {
                Response.Cookies.Delete("userCategory");
            }
            return RedirectToAction(nameof(SignIn));
        }

        [HttpPost]
        public IActionResult SignIn(String email, string password)
        {

            try
            {
                using (FundRaiserDbContext db = new FundRaiserDbContext())
                {
                    User u = (from user in db.User
                              where user.Email == email
                              select user).SingleOrDefault();

                    if (u != null)
                    {

                        if (password.Equals(u.Password))
                        {
                            HttpContext.Response.Cookies.Append("userId",
                                u.Id.ToString(),
                                new Microsoft.AspNetCore.Http.CookieOptions
                                {
                                    Expires = DateTimeOffset.Now.AddMinutes(40)
                                });

                            HttpContext.Response.Cookies.Append("userCategory",
                                u.Category.ToString(),
                                new Microsoft.AspNetCore.Http.CookieOptions
                                {
                                    Expires = DateTimeOffset.Now.AddMinutes(40)
                                });

                                return RedirectToAction(nameof(WelcomePage));

                        }
                        return View();
                    }
                }
            }
            catch
            {
                throw;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}