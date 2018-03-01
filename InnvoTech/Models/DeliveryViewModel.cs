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

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name ="Primary Address")]
        public string Address { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string City { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string State { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Use a 5 or 9 digit zip code")]
        public string Zip { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public DateTime? Date { get; set; }
    }
}
