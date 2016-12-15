using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    public class OrderPayment
    {
        public virtual int OrderPaymentId { get; set; }
        public virtual Order Order { get; set; }
        public virtual User Creator { get; set; }
        public virtual int ProcessingId { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string TrackingNumber { get; set; }
        public virtual DateTimeOffset AddedDate { get; set; }
        public virtual string Comment { get; set; }
    }
}
