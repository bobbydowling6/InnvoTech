using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InnvoTech.Models;

namespace InnvoTech.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Index()
        {
            return Content("You can see this if you're signed in!");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ForgotPasswordSent()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser existingUser = await _signInManager.UserManager.FindByNameAsync(username);
                if (existingUser != null)
                {
                    if (await _signInManager.UserManager.CheckPasswordAsync(existingUser, password))
                    {
                       await _signInManager.SignInAsync(existingUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("username", "Username or password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("username", "Username or password is incorrect");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser();
                var userResult = await _signInManager.UserManager.CreateAsync(newUser);
                if (userResult.Succeeded)
                {
                    var passwordResult = await _signInManager.UserManager.AddPasswordAsync(newUser, password);
                    if (passwordResult.Succeeded)
                    {
                       await _signInManager.SignInAsync(newUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                       await _signInManager.UserManager.DeleteAsync(newUser);
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
        
