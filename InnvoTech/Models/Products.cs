using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class Products
    {
        public Products()
        {
            CartProducts = new HashSet<CartProducts>();
            Reviews = new HashSet<Review>();
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<LineItem> LineItems { get; set; }
    }
}
