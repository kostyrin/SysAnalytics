using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Mappings
{
    public class AppliedDiscountMap : ClassMap<AppliedDiscount>
    {
        public AppliedDiscountMap()
        {
            Table("applied_discounts");
            Id(x => x.AppliedDiscountID).Column("id").GeneratedBy.Identity();
            References(o => o.Order).Column("order_id").Cascade.None().Not.LazyLoad();
            Map(x => x.Type);
            Map(x => x.Code);
            Map(x => x.Percent);
        }
    }
}
