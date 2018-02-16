﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace InnvoTech.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;

        public object DefaultAuthenticationTypes { get; private set; }

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Index()
        {
            return Content("You can see this if you're signed in!");
        }

        public ActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser(username);
                var Result = _signInManager.UserManager.GetUserAsync(User).Result;
                if (Result.Succeeded)
                {
                    var passwordResult = _signInManager.UserManager.AddPasswordAsync(user, password).Result;
                    if (passwordResult.Succeeded)
                    {
                        _signInManager.SignInAsync(user, false).Wait();
                        return RedirectToAction("Index", "home");
                    }
                    else
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        _signInManager.UserManager.DeleteAsync(user).Wait();
                    }
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View();
        }    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string username, string password)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser(username);
                var userResult = _signInManager.UserManager.CreateAsync(newUser).Result;
                if (userResult.Succeeded)
                {
                    var passwordResult = _signInManager.UserManager.AddPasswordAsync(newUser, password).Result;
                    if (passwordResult.Succeeded)
                    {
                        _signInManager.SignInAsync(newUser, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        _signInManager.UserManager.DeleteAsync(newUser).Wait();
                    }
                }
                else
                {
                    foreach (var error in userResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View();
        }
    }
}
        
