using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartProducts = new HashSet<CartProducts>();
        }

        public int Id { get; set; }
        public Guid TrackingNumber { get; set; }
        public ApplicationUser User { get; set; }       
        public ICollection<CartProducts> CartProducts { get; set; }
    }
}
