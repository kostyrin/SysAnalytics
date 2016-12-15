using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    public class Emailing
    {
        public virtual int EmailingId { get; set; }
        //public virtual User Creator { get; set; }
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

        //public virtual bool IsDelivered
        //{
        //    get { return (this.DeliveryDate - DateTime.UtcNow).Minutes > 1; }
        //}
    }
}
