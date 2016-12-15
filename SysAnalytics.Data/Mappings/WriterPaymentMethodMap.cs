using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Mappings
{
    public class WriterPaymentMethodMap : ClassMap<WriterPaymentMethod>
    {
        public WriterPaymentMethodMap()
        {
            Table("writer_payment_methods");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Name).Column("name");
            Map(x => x.TransferMinimal).Column("transfer_min").Nullable();
            Map(x => x.TransferDefault).Column("transfer_default").Nullable();
            Map(x => x.AvailFromMoney).Column("avail_from_money").Nullable();
            Map(x => x.AvailFromDays).Column("avail_from_days").Nullable();
        }
    }
}
