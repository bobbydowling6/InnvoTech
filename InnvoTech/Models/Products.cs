using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class Products
    {
        public Products()
        {
            CartProducts = new HashSet<CartProducts>();
            OrderProducts = new HashSet<OrderProducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
