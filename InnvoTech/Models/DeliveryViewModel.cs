using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public class DeliveryViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
    }
}
