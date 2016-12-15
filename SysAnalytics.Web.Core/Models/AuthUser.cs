using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using NHibernate.AspNet.Identity;
using SelenaAnalitics.Model;

namespace SelenaAnalitics.Web.Core.Models
{ 
    [Serializable]
    public class AuthUser  : IdentityUser
    {
        //public int Id { get; set; }
        //public string UserName { get; set; }
        //public string RoleName { get;  set; }
        //public string Email { get; set; }
        //public virtual string Pass { get; set; }
        //public virtual string ConfirmPass { get; set; }

        //public virtual bool IsAuthenticated
        //{
        //    get { return true; }
        //}

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AuthUser, string> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    return await Task<ClaimsIdentity>.Factory.StartNew(() =>
        //    {
        //        IList<Claim> claimCollection = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, this.UserName),
        //            new Claim(ClaimTypes.Email, this.Email),
        //            //new Claim(ClaimTypes.Role, this.Roles), //Its user by SelenaAnaliticsAuthorize
        //            new Claim(ClaimTypes.NameIdentifier, this.Id.ToString()),
        //            new Claim(@"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", this.Id.ToString())
        //        };

        //        return new ClaimsIdentity(claimCollection, DefaultAuthenticationTypes.ApplicationCookie);
        //    });
        //}

        //public AuthUser(){}
        //public AuthUser(string UserName, int userId)
        //{
        //    this.UserName = UserName;
        //    this.Id = userId;
        //}
        //public AuthUser(string UserName, int userId, string roleName)
        //{
        //    this.UserName = UserName;
        //    this.Id = userId;
        //    this.RoleName = roleName;
        //}

        //public AuthUser(User user)
        //{
        //    this.Id = user.UserId;
        //    this.RoleName = Enum.GetName(typeof(UserRoles), user.RoleId);
        //    //this.UserName = user.DisplayName;
        //    this.Email = user.Email;
        //}

        //public override string ToString()
        //{
        //    return UserName;
        //}
    }
}
