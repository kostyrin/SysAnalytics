using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Mappings
{
    public class AvailableTimeMap : ComponentMap<AvailableTime>
    {
        public AvailableTimeMap()
        {
            //Map(o => o.FromHour).Column("hometime_from");
            //Map(o => o.ToHour).Column("hometime_to");
            Map(o => o.FromHour);
            Map(o => o.ToHour);
        }
    }
}
