using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProducts>();
        }

        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Email { get; set; }
        public string PurchaserName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingAndHandling { get; set; }
        public decimal Tax { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime? ShipDate { get; set; }

        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
