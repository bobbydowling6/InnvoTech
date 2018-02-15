using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InnvoTech.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Models.ProductsViewModel model = new Models.ProductsViewModel();
            model.Name = "The Biski";
            model.Id = 1;
            model.Color = "Blue or Red";
            model.Price = 3000;
            model.Description = "The Biski is truly unique; as a single seat (or single plus pillion), twin jet, HSA Motorcycle, it is a world’s first in many ways. At just 2.3m long and under 1m wide, it is the smallest of all Gibbs High speed amphibious platforms, and very probably the most technically advanced. It represents true freedom for the individual; serious fun.";
            model.ImageUrl = "/images/biski.jpg";
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string id)
        {
            string cartId;
            if (!Request.Cookies.ContainsKey("cartid"))
            {
                cartId = Guid.NewGuid().ToString();
                Response.Cookies.Append("cartId", cartId, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddYears(1) });
            }
            else
            {
                Request.Cookies.TryGetValue("cartId", out cartId);
            }
            Console.WriteLine("added {0} to cart {1}", id, cartId);

            return RedirectToAction("Index", "Delivery");
        }
    }
}