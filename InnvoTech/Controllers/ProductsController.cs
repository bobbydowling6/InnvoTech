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
        public IActionResult Index(int? id)
        {
            //ADO.Net method of calling SQL database to show list of products to viewing page
            Models.ProductsViewModel model = new Models.ProductsViewModel();


            string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = BobTest; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            var connection = new System.Data.SqlClient.SqlConnection(connectionString);

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "Select * From Products Where ID = " + id.Value;
            var reader = command.ExecuteReader();
            var nameColumn = reader.GetOrdinal("Name");
            var priceColumn = reader.GetOrdinal("Price");
            var colorColumn = reader.GetOrdinal("Color");
            var descriptionColumn = reader.GetOrdinal("Description");
            var imageUrlColumn = reader.GetOrdinal("ImageUrl");
            while (reader.Read())
            {
                model.Name = reader.IsDBNull(nameColumn) ? "" : reader.GetString(nameColumn);   //I can see name is the second column in the database.
                model.Price = reader .IsDBNull(priceColumn) ? 0m: reader.GetDecimal(priceColumn);
                model.Color = reader.GetString(colorColumn);
                model.Description = reader.GetString(descriptionColumn);
                model.ImageUrl = reader.GetString(imageUrlColumn);
            }

            //model.Name = "The Biski";
            //model.Id = 1;
            //model.Color = "Blue or Red";
            //model.Price = 3000;
            //model.Description = "The Biski is truly unique; as a single seat (or single plus pillion), twin jet, HSA Motorcycle, it is a world’s first in many ways. At just 2.3m long and under 1m wide, it is the smallest of all Gibbs High speed amphibious platforms, and very probably the most technically advanced. It represents true freedom for the individual; serious fun.";
            //model.ImageUrl = "/images/biskigify.gif";
            connection.Close();
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

            byte[] objectBytes = null;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                //I'm going to serialize the product name here, but you can serialize anything - including complex objects!
                bf.Serialize(ms, id);
                objectBytes = ms.ToArray();
            };

            HttpContext.Session.Set(cartId, objectBytes);

            return RedirectToAction("Index", "Delivery");
        }
    }
}