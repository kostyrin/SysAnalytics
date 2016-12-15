using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Model;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;

namespace SysAnalytics.Web.ViewModels
{
    public class EmalingStatisticFormModel
    {
        [JQGridColumn(IsSortable = true, Width = 100, Formatter = JSFormatters.Integer)]
        public virtual int RecipientId { get; set; }
        [JQGridColumn(Width = 300)]
        public virtual string Emailing_Template { get; set; }
        [JQGridColumn(Width = 150, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Emailing_DeliveryDate { get; set; }
        [JQGridColumn(Width = 150, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Emailing_AddedDate { get; set; }
        [JQGridColumn(Width = 70)]
        public virtual string Opens { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70)]
        public virtual string Clicks { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select, Width = 120)]
        public virtual EmailDetailStatus ResultStatus { get; set; }
        [JQGridColumn(IsSortable = true, Width = 120)]
        public virtual string RejectReason { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70)]
        public virtual string State { get; set; }

        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer, IsHidden = true)]
        public virtual int EmailingDetailId { get; set; }
        [JQGridColumn(IsSortable = true, Width = 400, IsHidden = true)]
        public virtual string EmailId { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select, Width = 150, IsHidden = true)]
        public virtual EmailStatus Emailing_Status { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Integer, IsHidden = true)]
        public virtual int Emailing_CreatorId { get; set; }
        //[JQGridColumn(Width = 70, Formatter = JSFormatters.Integer, IsHidden = true)]
        //public virtual int Emailing_UserCount { get; set; }
        [JQGridColumn(Width = 90, Formatter = JSFormatters.Integer, IsHidden = true)]
        public virtual int Emailing_EmailingId { get; set; }
    }
}