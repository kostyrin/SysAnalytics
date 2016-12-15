using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model
{
    public class Site
    {
        public virtual int SiteId { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
