using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Mappings
{
    public class EmailingDetailMap : ClassMap<EmailingDetail>
    {
        public EmailingDetailMap()
        {
            Table("emailing_details");
            Id(e => e.EmailingDetailId).Column("id").GeneratedBy.Identity();
            Map(e => e.EmailId).Column("email_id");
            Map(e => e.RejectReason).Column("reject_reason");
            Map(e => e.ResultStatus).Column("result_status");
            Map(e => e.RecipientId).Column("recipient");
            References(o => o.Emailing).Column("emailing_id");
        }
    }
}
