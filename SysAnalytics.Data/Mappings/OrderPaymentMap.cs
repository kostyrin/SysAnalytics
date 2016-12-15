using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model.Entities;
using SysAnalytics.Model.UserTypes;

namespace SysAnalytics.Data.Mappings
{
    public class OrderPaymentMap : ClassMap<OrderPayment>
    {
        public OrderPaymentMap()
        {
            Table("order_payments");
            Id(x => x.OrderPaymentId).Column("id").GeneratedBy.Identity();
            References(o => o.Order).Column("order_id").Cascade.None().Not.LazyLoad();
            References(o => o.Creator).Column("creator_id").Cascade.None().Not.LazyLoad();
            Map(x => x.ProcessingId).Column("processing_id");
            Map(x => x.Total);
            Map(x => x.TrackingNumber).Column("tracking_number");
            Map(o => o.AddedDate).Column("added").CustomType<UnixTimeUserType>().Nullable();
            Map(x => x.Comment);
        }
    }
}
