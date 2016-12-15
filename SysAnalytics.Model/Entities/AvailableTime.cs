using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    public class AvailableTime
    {
        /// <summary>
        /// Available from that hour
        /// </summary>
        //public virtual int? FromHour { get; set; }
        public virtual int FromHour { get; set; }

        /// <summary>
        /// Available to that hour
        /// </summary>
        //public virtual int? ToHour { get; set; }
        public virtual int ToHour { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.FromHour, this.ToHour);
        }
    }
}
