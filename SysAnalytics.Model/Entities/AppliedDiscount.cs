using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    public class AppliedDiscount
    {
        public virtual int AppliedDiscountID { get; set; }
        public virtual Order Order { get; set; }
        public virtual string Type { get; set; }
        public virtual string Code { get; set; }
        public virtual decimal Percent { get; set; }
    }
}
