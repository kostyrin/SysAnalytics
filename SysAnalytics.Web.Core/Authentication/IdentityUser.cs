using System;
using System.Collections.Generic;
using MySql.AspNet.Identity;


namespace SysAnalytics.Web.Core.Authentication
{
    public class IdentityUser : MySql.AspNet.Identity.IdentityUser
    {
        public virtual string Templates { get; set; }

        public IdentityUser()
        {
            this.Claims = new List<IdentityUserClaim>();
            this.Roles = new List<string>();
            //this.Logins = new List<UserLoginInfo>();
            this.Id = Guid.NewGuid().ToString();
            LockoutEnabled = true;
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }
    }

    public sealed class IdentityUserLogin
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
    }

    
}
