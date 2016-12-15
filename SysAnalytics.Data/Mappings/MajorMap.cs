using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Mappings
{
    public class MajorMap : ClassMap<Major>
    {
        public MajorMap()
        {
            Table("majors");
            Id(o => o.MajorId).Column("major").GeneratedBy.Identity();
            Map(o => o.Name).Column("name").Nullable();
            //References(o => o.Parent).Column("parent");
        }
    }
}
