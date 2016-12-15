using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    public class WriterPaymentMethod
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal? TransferMinimal { get; set; }
        public virtual decimal? TransferDefault { get; set; }
        public virtual decimal? AvailFromMoney { get; set; }
        public virtual int? AvailFromDays { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
