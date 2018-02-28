using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnvoTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InnvoTech.Controllers
{
    public class DeliveryController : Controller
    {
        private BobTestContext _context;

        public DeliveryController(BobTestContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string cartId;
            Guid trackingNumber;
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out trackingNumber))
            {
                var cart = _context.Cart.Include(x => x.CartProducts).ThenInclude(y => y.Products).Single(x => x.TrackingNumber == trackingNumber);
                ViewData["gadgetName"] = cart.CartProducts.First().Products.Name;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DeliveryViewModel models)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{5}(?:[-\s]\d{4})?$");
            if (string.IsNullOrEmpty(models.Zip) || !regex.IsMatch(models.Zip))
            {
                ModelState.AddModelError("Zip", "The zip code is invalid!");
            }

            if (ModelState.IsValid)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}

