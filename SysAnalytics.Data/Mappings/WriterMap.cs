using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using SysAnalytics.Model.Entities;
using SysAnalytics.Model.Enums;
using SysAnalytics.Model.UserTypes;

namespace SysAnalytics.Data.Mappings
{
    public class WriterMap : SubclassMap<Writer>
    {
        public WriterMap()
        {
            Table("writers");
            Map(c => c.WriterId).Column("user_id");
            Map(w => w.History);
            Map(w => w.WorkStatus).Column("wh_status");
            Map(w => w.HireHistory).Column("hirehist");
            Map(w => w.Gender);
            Map(w => w.MaritalStatus).Column("marital");
            Map(w => w.WorkHoursGrade).Column("work_hours");
            Map(w => w.Iam).Column("i_am");
            Map(w => w.SalaryPeriodicity).Column("salary");
            Map(w => w.AssigmentsDone).Column("assignments_done");
            Map(w => w.WriteOther).Column("write_other");
            Map(w => w.Enjoy);
            Map(w => w.Dislike);
            Map(w => w.City);
            Map(w => w.State);
            Map(w => w.HireReason).Column("hire_reason");
            Map(w => w.Occupation);
            Map(w => w.Education);
            Map(w => w.StatusBalance);
            Map(w => w.Status);
            Map(w => w.IsTracked).Column("is_tracked");
            Map(x => x.ActivationDate).CustomType<NullableTimestampUserType>().Nullable().Column("activation_date");
            Map(x => x.Status1stSetDate).CustomType<NullableTimestampUserType>().Nullable().Column("status1st_set");
            Map(x => x.DeactivationDate).CustomType<NullableTimestampUserType>().Nullable().Column("deactivation_date");
            Map(x => x.StatusBalanceTmp).Column("statusbalance_tmp");
            Map(w => w.CanSeePartners).Column("can_see_partners");
            //Component(x => x.TimeAvailableAtHome);
            //Component(x => x.TimeAvailableAtWork);
            //Component(x => x.TimeAvailableAtCell);
            Map(w => w.TimeAvailableAtHomeFromHour).Column("hometime_from");
            Map(w => w.TimeAvailableAtHomeToHour).Column("hometime_to");
            Map(w => w.TimeAvailableAtWorkFromHour).Column("worktime_from");
            Map(w => w.TimeAvailableAtWorkToHour).Column("worktime_to");
            Map(w => w.TimeAvailableAtCellFromHour).Column("celltime_from");
            Map(w => w.TimeAvailableAtCellToHour).Column("celltime_to");
            References(w => w.Sample).NotFound.Ignore().Column("samples");
            //Map(w => w.WriterPaymentMethodId).Column("writer_payment_method").Nullable();
            //References(w => w.WriterPaymentMethod).Column("writer_payment_method").Cascade.All();
            Map(w => w.PaymentMethod1).Column("payment1");
            Map(w => w.PaymentMethod2).Column("payment2");

            //DiscriminatorValue(1);
        }
    }
}
