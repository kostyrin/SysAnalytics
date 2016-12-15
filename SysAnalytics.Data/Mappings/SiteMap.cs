using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model;

namespace SysAnalytics.Data.Mappings
{
    public class SiteMap : ClassMap<Site>
    {
        public SiteMap()
        {
            Table("sites");
            Id(x => x.SiteId).Column("id").GeneratedBy.Identity();
            Map(x => x.Name);
        }
        
    }
}
