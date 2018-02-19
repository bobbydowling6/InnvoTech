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
            string cartId;
            if (Request.Cookies.TryGetValue("cartId", out cartId))
            {
                byte[] gadgetBytes;
                if(HttpContext.Session.TryGetValue(cartId, out gadgetBytes))
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        ms.Write(gadgetBytes, 0, gadgetBytes.Length);
                        ms.Seek(0, System.IO.SeekOrigin.Begin);
                        ViewData["gadgetName"] = (string)bf.Deserialize(ms);
                    }
                }
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