using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class CartProducts
    {
        public Guid CartId { get; set; }
        public int ProductsId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

        public Cart Cart { get; set; }
        public Products Products { get; set; }
    }
}
