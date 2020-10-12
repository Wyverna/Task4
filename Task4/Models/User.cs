using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Task4.Models
{
    public class User:IdentityUser
    {
        public DateTime Registration { get; set; }
        public DateTime LastLogin { get; set; }
        public string Status { get; set; }

        public User()
        {
            Status = "Unblocked";
            Registration = DateTime.Now;
        }
    }
}
