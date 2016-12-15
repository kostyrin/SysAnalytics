using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MySql.AspNet.Identity;
using NHibernate;
using IdentityUser = SysAnalytics.Web.Core.Authentication.IdentityUser;

//using NHibernate.AspNet.Identity;


// using NHibernate.AspNet.Identity;

namespace SysAnalytics.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationUserStore : MySqlUserStore<ApplicationUser>
    {
        
    }
}