using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartProducts = new HashSet<CartProducts>();
        }

        public Guid Id { get; set; }
        public string AspNetUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; }
    }
}
