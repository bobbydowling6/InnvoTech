using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Comments { get; set; }
    }
}
