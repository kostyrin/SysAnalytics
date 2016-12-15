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
    public class EmailingMap : ClassMap<Emailing>
    {
        public EmailingMap()
        {
            Table("emailing_history");
            Id(e => e.EmailingId).Column("id").GeneratedBy.Identity();
            Map(e => e.CreatorId).Column("creator");
            Map(e => e.AddedDate).Column("added_date").CustomType<UnixTimeUserType>().Nullable();
            Map(e => e.DeliveryDate).Column("delivery_date").CustomType<UnixTimeUserType>().Nullable();
            Map(e => e.Status);
            Map(e => e.UserCount).Column("user_count");
            Map(e => e.Template);
            Map(e => e.SentEmails).Column("sent");
            Map(e => e.QueuedEmails).Column("queued");
            Map(e => e.RejectedEmails).Column("rejected");
            Map(e => e.InvalidEmails).Column("invalid");
            Map(e => e.ScheduledEmails).Column("scheduled");
        }
    }
}
