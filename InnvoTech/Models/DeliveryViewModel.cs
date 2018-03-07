using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public class DeliveryViewModel
    {
        public CartProducts[] CartProducts { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "You need to enter an address")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Your Address")]
        public string ShippingAddress { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string ShippingCity { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string ShippingState { get; set; }
        [Required]
        public string ShippingZip { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public DateTime? Date { get; set; }
        [Required]
        [CreditCard]
        public string creditcardnumber { get; set; }
        [Required]
        public string creditcardname { get; set; }
        [Required]
        public string creditcardverificationvalue { get; set; }
        [Required]
        public string expirationmonth { get; set; }
        [Required]
        public string expirationyear { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string BillingCity { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string BillingState { get; set; }
        [Required]
        public string BillingZip { get; set; }
    }
}
