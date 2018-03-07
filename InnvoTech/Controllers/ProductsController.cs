using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using InnvoTech.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InnvoTech.Controllers
{
    public class ProductsController : Controller
    {
        //Part of using Ado.net
        //private ConnectionStrings _connectionStrings;
        private BobTestContext _context;

        //public ProductsController(IOptions<ConnectionStrings> connectionStrings)
        //{
        //    _connectionStrings = connectionStrings.Value;
        //}
        public ProductsController(BobTestContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Gadgets ()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id = 1)
        {
            //ADO.Net method of calling SQL database to show list of products to viewing page, including using a stored procedure
            //ProductsViewModel model = new ProductsViewModel();
            var product = await _context.Products.Include(x => x.Reviews).SingleAsync(x => x.Id == id);
            return View(product);
            //Another method to use to call out all products
            //if (id.HasValue)
            //{
            //    return View(_context.Products.Where(x => x.id == id.HasValue));
            //}
            //else
            //{
            //    return View(_context.Products);
            //}
        }
        //     using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
        //    {
        //        connection.Open();
        //        var command = connection.CreateCommand();

        //        command.CommandText = "sp_GetProduct";
        //        command.Parameters.AddWithValue("@id", id);
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        using (var reader = command.ExecuteReader())
        //        {
        //            var nameColumn = reader.GetOrdinal("Name");
        //            var priceColumn = reader.GetOrdinal("Price");
        //            var descriptionColumn = reader.GetOrdinal("Description");
        //            var imageUrlColumn = reader.GetOrdinal("ImageUrl");
        //            while (reader.Read())
        //            {
        //                model.Name = reader.IsDBNull(nameColumn) ? "" : reader.GetString(nameColumn);   //I can see name is the second column in the database.
        //                model.Price = reader.IsDBNull(priceColumn) ? 0m : reader.GetDecimal(priceColumn);
        //                model.Description = reader.IsDBNull(descriptionColumn) ? "" : reader.GetString(descriptionColumn);
        //                model.ImageUrl = reader.IsDBNull(imageUrlColumn) ? "/image/noImage.jpg" : reader.GetString(imageUrlColumn);
        //            }
        //        }

        //        //model.Name = "The Biski";
        //        //model.Id = 1;
        //        //model.Color = "Blue or Red";
        //        //model.Price = 3000;
        //        //model.Description = "The Biski is truly unique; as a single seat (or single plus pillion), twin jet, HSA Motorcycle, it is a world’s first in many ways. At just 2.3m long and under 1m wide, it is the smallest of all Gibbs High speed amphibious platforms, and very probably the most technically advanced. It represents true freedom for the individual; serious fun.";
        //        //model.ImageUrl = "/images/biskigify.gif";
        //        connection.Close();
        //    }
        //    return View(model);
        //}
        

        [HttpPost]
        public async Task<IActionResult> Index(int id, bool extraParam = true)
        {
            Guid cartId;
            Cart c;
            CartProducts i;

            if (Request.Cookies.ContainsKey("cartId") && Guid.TryParse(Request.Cookies["cartId"], out cartId) && _context.Cart.Any(x => x.TrackingNumber == cartId))
            {
                c = await _context.Cart
                    .Include(x => x.CartProducts)
                    .ThenInclude(y => y.Products)
                    .SingleAsync(x => x.TrackingNumber == cartId);
            }
            else
            {
                c = new Cart();
                cartId = Guid.NewGuid();
                c.TrackingNumber = cartId;
                _context.Cart.Add(c);
            }
            if (User.Identity.IsAuthenticated)
            {
                c.User = await _context.Users.FindAsync(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            }
            if (c.CartProducts.Any(x => x.Products.Id == id))
            {
                i = c.CartProducts.FirstOrDefault(x => x.Products.Id == id);
            }
            else
            {
                i = new CartProducts();
                i.Cart = c;
                i.Products = _context.Products.Find(id);
                c.CartProducts.Add(i);
            }
            i.Quantity++;

            _context.SaveChanges();
            Response.Cookies.Append("cartId", c.TrackingNumber.ToString(),
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });

            //Console.WriteLine("Added {0} to cart {1}", id, cartId);


            return RedirectToAction("Index", "Delivery");
        }
    }
}