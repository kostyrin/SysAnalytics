using System;
using System.ComponentModel.DataAnnotations;
using SysAnalytics.Model;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Web.ViewModels
{
    public class UserFormModel
    {
        public virtual int UserId { get; set; }
        //public virtual string Email { get; set; }
        //public virtual string FirstName { get; set; }
        //public virtual string LastName { get; set; }
        //public virtual string Password { get; set; }
        //public virtual string ConfirmPassword { get; set; }
        //public virtual string PasswordHash { get; set; }
        //public virtual int RegDate { get; set; }
        //public virtual DateTimeOffset? LastLoginTime { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int RoleId { get; set; }
        public virtual string Country { get; set; }
        public virtual int TimeZone { get; set; }
        public virtual int PaymentMethod { get; set; }
        public virtual string PaymentDetails { get; set; }
        public virtual bool DisableNotifications { get; set; }
        public virtual Site Site { get; set; }
        public virtual FindUs FindUs { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual bool IsProfileEditing { get; set; }
        public virtual DateTimeOffset BirthDate { get; set; }
        public virtual bool IsFrozen { get; set; }
    }
}