using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnvoTech.Models;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using Microsoft.AspNetCore.Http.Extensions;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace InnvoTech.Controllers
{
    public class ContactController : Controller
    {
        private SendGridClient _sendGridClient;

        public ContactController(SendGrid.SendGridClient sendGridClient)
        {
            this._sendGridClient = sendGridClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string user)
        {
            var apiKey = System.Environment.GetEnvironmentVariable("sendgrid");
            var client = new SendGridClient("sendgrid");
            SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
            {
                message.AddTo(new EmailAddress(("bobdowling0@gmail.com")));
                message.Subject = "Contact Form";
                message.SetFrom("innvotech@codingtemplestudent.com");
                message.AddContent("text/plain", "Thank You for filling out this contact form" + user + "We will get back to you very shortly");
                message.AddContent("text/html", "Thank You for filling out this contact form" + user + "We will get back to you very shortly");
                message.SetTemplateId("cae92438-518b-473a-b985-fd2f66b7c947");
            };
            await _sendGridClient.SendEmailAsync(message);
            return RedirectToAction("Index", "Home");
        }
    }
}