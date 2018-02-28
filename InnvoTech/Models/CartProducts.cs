using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class CartProducts
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        
        public Cart Cart { get; set; }
        public Products Products { get; set; }
    }
}
