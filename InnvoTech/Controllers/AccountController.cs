using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InnvoTech.Models;
using SendGrid;

namespace InnvoTech.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private SendGridClient _sendGridClient;

        public AccountController(SignInManager<ApplicationUser> signInManager, SendGrid.SendGridClient sendGridClient)
        {
            this._signInManager = signInManager;
            this._sendGridClient = sendGridClient;
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
                newUser.UserName = username;
                var userResult = await _signInManager.UserManager.CreateAsync(newUser);
                if (userResult.Succeeded)
                {
                    var passwordResult = await _signInManager.UserManager.AddPasswordAsync(newUser, password);
                    if (passwordResult.Succeeded)
                    {
                        //TODO: Send a user a message thanking them for registering.
                        SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                        message.AddTo(username);
                        message.Subject = "Welcome to InnvoTech";
                        message.SetFrom("innvotech@codingtemplestudent.com");
                        message.AddContent("text/plain", "Thanks for registering as " + username + " on InnvoTech!");
                        await _sendGridClient.SendEmailAsync(message);

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
        
