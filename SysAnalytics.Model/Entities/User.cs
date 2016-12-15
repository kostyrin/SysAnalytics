using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using SysAnalytics.Core.Common;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Model
{

    public class User
    {
        public virtual int UserId { get; set; }
        //public virtual string Email { get; set; }
        //public virtual string FirstName { get; set; }
        //public virtual string LastName { get; set; }
        public virtual DateTimeOffset RegDate { get; set; }
        public virtual DateTimeOffset LastLogin { get; set; }
        public virtual bool IsActive { get; set; }
        //public virtual int RoleId { get; set; }
        public virtual string Country { get; set; }
        public virtual int TimeZone { get; set; }
        //public virtual int PaymentMethod { get; set; }
        public virtual string PaymentDetails { get; set; }
        public virtual bool DisableNotifications { get; set; }
        public virtual Site Site { get; set; }
        public virtual FindUs FindUs { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual bool IsProfileEditing { get; set; }
        public virtual DateTimeOffset BirthDate { get; set; }
        public virtual bool IsFrozen { get; set; }

        public override string ToString()
        {
            return this.UserId.ToString();
        }

    }
}
