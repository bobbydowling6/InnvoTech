using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public class DeliveryViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name ="Your Address")]
        public string Address { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string City { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int Zip { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string State { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public DateTime Date { get; set; }
    }
}
