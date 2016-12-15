using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Model;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;

namespace SysAnalytics.Web.ViewModels
{
    public class WriterFormModel
    {
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int WriterId { get; set; }
        [JQGridColumn(IsHidden = true)]
        public virtual string History { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual WorkStatus WorkStatus { get; set; }
        [JQGridColumn(IsHidden = true)]
        public virtual string HireHistory { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual Gender Gender { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual MaritalStatus MaritalStatus { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeAvailableAtHome_FromHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeAvailableAtHome_ToHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeAvailableAtWork_FromHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeAvailableAtWork_ToHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeAvailableAtCell_FromHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeAvailableAtCell_ToHour { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual WorkHoursScale WorkHoursGrade { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual WriterOcupation Iam { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual SalaryPeriodicity SalaryPeriodicity { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int AssigmentsDone { get; set; }
        //public virtual Attachment Resume { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Sample { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool WriteOther { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Enjoy { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Dislike { get; set; }
        //[JQGridColumn(IsHidden = false)]
        //public virtual string WriterPaymentMethod { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual int WriterPaymentMethodId { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual PaymentMethod2 PaymentMethod1 { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual PaymentMethod2 PaymentMethod2 { get; set; }
        //[JQGridColumn(IsHidden = false)]
        //public virtual string Employers { get; set; }
        [JQGridColumn(IsHidden = true)]
        public virtual string HireReason { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Occupation { get; set; }
        [JQGridColumn(IsHidden = true)]
        public virtual string Education { get; set; }
        //[JQGridColumn(IsHidden = false)]
        //public virtual string Address { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string City { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string State { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal StatusBalance { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal StatusBalanceTmp { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Status { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? ActivationDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? DeactivationDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? Status1stSetDate { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsTracked { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool CanSeePartners { get; set; }

        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset RegDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset LastLogin { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsActive { get; set; }
        [JQGridColumn(Entity = "Writer")]
        public virtual string Country { get; set; }
        [JQGridColumn(Entity = "Writer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeZone { get; set; }
        //[JQGridColumn(Entity = "Writer", IsHidden = true)]
        //public virtual string PaymentDetails { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool DisableNotifications { get; set; }
        [JQGridColumn(Entity = "Writer", IsSortable = true)]
        public virtual string Site_Name { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Select)]
        public virtual FindUs FindUs { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Select)]
        public virtual UserType UserType { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsProfileEditing { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset BirthDate { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsFrozen { get; set; }
    }
}