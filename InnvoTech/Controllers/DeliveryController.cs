using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnvoTech.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnvoTech.Controllers
{
    public class DeliveryController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string gadgetName = "your cool gadget";
            Request.Cookies.TryGetValue("productID", out gadgetName);
            ViewData["gadgetName"] = gadgetName;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DeliveryViewModel models)
        { if (ModelState.IsValid)
            {
                return this.RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}