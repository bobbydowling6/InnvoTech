using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
