using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Model;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;

namespace SysAnalytics.Web.ViewModels
{
    public class CustomerFormModel
    {
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int CustomerId { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Employed { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string MainMajors { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string StudyLevel { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual double CurrentGPA { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual double DesiredGPA { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool EnglishNative { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int EnglishStudyYears { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Difficulties { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string CommonMistakes { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select)]
        public virtual CustomerQualityExpectations CustomerQualityExpectations { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string AdditionalComments { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsWroteReview { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool SendTips { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool SendSeasonal { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int NumCompletedOrders { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int NumCompletedPages { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal Bonus { get; set; }
        //public virtual Customer Tempter { get; set; }
        //public virtual Attachment Portfolio { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Emergency { get; set; }
        //public virtual int LastSendedFileId { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool? IsSubscriber { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool DeniedCustomer { get; set; }
        //public virtual EnglishDialect EnglishDialect { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsPartner { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal BalancePages { get; set; }
        //public virtual string Address { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string City { get; set; }
        //public virtual string ZIPCode { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual Degree Degree { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string CountryStudy { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select, Width = 50, Align = JSAlign.Center)]
        public virtual EnglishLevel EnglishLevel { get; set; }

        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset RegDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset LastLogin { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsActive { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Country { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int TimeZone { get; set; }
        //[JQGridColumn(Entity = "Customer", IsHidden = true)]
        //public virtual string PaymentDetails { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool DisableNotifications { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true)]
        public virtual string Site_Name { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select)]
        public virtual FindUs FindUs { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select)]
        public virtual UserType UserType { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsProfileEditing { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset BirthDate { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsFrozen { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int LifeTimeDiscount { get; set; }
    }
}
