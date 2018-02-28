using System;
using System.Collections.Generic;

namespace InnvoTech.Models
{
    public partial class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public Guid TrackingNumber { get; set; }
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

        public ApplicationUser User { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
