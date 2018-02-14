using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InnvoTech.Controllers
{
    public class ProductsController : Controller
    {
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
    }
}