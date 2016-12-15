using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Model;

namespace SysAnalytics.Web.Core.ViewModels
{
    public class EmailingInfoDto
    {
        public virtual int EmailingDetailId { get; set; }
        public virtual string EmailId { get; set; }
        public virtual string RejectReason { get; set; }
        public virtual EmailDetailStatus ResultStatus { get; set; }
        public virtual int RecipientId { get; set; }
        public virtual string State { get; set; }
        public virtual string Opens { get; set; }
        public virtual string Clicks { get; set; }
    }
}
