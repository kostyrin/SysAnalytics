using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Model;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;

namespace SysAnalytics.Web.ViewModels
{
    public class EmailingDetailsFormModel
    {
        public virtual int CreatorId { get; set; }
        public virtual DateTimeOffset AddedDate { get; set; }
        public virtual DateTimeOffset DeliveryDate { get; set; }
        public virtual EmailStatus Status { get; set; }
        public virtual int UserCount { get; set; }
        public virtual string Template { get; set; }
        public virtual int SentEmails { get; set; }
        public virtual int QueuedEmails { get; set; }
        public virtual int RejectedEmails { get; set; }
        public virtual int InvalidEmails { get; set; }
        public virtual int ScheduledEmails { get; set; }
        public virtual int EmailingId { get; set; }
        public List<EmailingInfoDto> Details { get; set; }
    }
}