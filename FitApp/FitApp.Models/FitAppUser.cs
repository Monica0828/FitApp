using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace FitApp.Models
{
    public class FitAppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

    }
}
