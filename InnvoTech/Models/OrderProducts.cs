using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class OrderProducts
    {
        public int OrderId { get; set; }
        public int ProductsId { get; set; }
        public int Quantity { get; set; }
        public string PlacedName { get; set; }
        public decimal PlacedUnitPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

        public Order Order { get; set; }
        public Products Products { get; set; }
    }
}
