using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public class LineItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public Products products { get; set; }
        public Order Order { get; set; }
    }
}
