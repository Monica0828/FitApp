using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FitApp.Models
{
    public class FitAppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<FitAppUser> manager)
        {

            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("FirstName", FirstName));
            return userIdentity;
        }
    }
    public static class IdentityExtensions
    {
        public static string GetFirstName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue("FirstName");
            }
            return null;
        }
    }

}
