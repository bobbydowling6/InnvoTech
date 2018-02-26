using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnvoTech.Models
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
        }
}

