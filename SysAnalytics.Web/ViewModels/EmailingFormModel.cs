using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Model;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;

namespace SysAnalytics.Web.ViewModels
{
    public class EmailingFormModel
    {
        //public virtual User Creator { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer)]
        public virtual int CreatorId { get; set; }
        [JQGridColumn(Width = 90, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset AddedDate { get; set; }
        [JQGridColumn(Width = 90, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset DeliveryDate { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual EmailStatus Status { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer)]
        public virtual int UserCount { get; set; }
        [JQGridColumn(IsSortable = true, Width = 400)]
        public virtual string Template { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer)]
        public virtual int SentEmails { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer)]
        public virtual int QueuedEmails { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer)]
        public virtual int RejectedEmails { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer)]
        public virtual int InvalidEmails { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Formatter = JSFormatters.Integer)]
        public virtual int ScheduledEmails { get; set; }
        [JQGridColumn(IsSortable = true, Width = 90, Formatter = JSFormatters.ButtonDetails)]
        public virtual int EmailingId { get; set; }
    }
}