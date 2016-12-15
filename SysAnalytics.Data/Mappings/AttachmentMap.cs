using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Mappings
{
    public class AttachmentMap :ClassMap<Attachment>
    {
        public AttachmentMap()
        {
            Id(x => x.AttachmentId).Column("id").GeneratedBy.Identity();
            Map(x => x.OriginalFileName).Column("fname");
            Map(x => x.IsHidden).Column("is_hidden");
        }
    }
}
