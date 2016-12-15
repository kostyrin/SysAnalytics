using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Cfg.XmlHbmBinding;
using SysAnalytics.Model.Entities;
using SysAnalytics.Model.UserTypes;

namespace SysAnalytics.Data.Mappings
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(o => o.OrderId).Column("id").GeneratedBy.Identity();
            Map(o => o.CustomerId).Column("customer_id");
            Map(o => o.WriterId).Not.LazyLoad().Column("assigned_to");
            References(o => o.Customer).Column("customer_id").NotFound.Ignore();
            References(o => o.Writer).Column("assigned_to").NotFound.Ignore();
            Map(o => o.AssignDate).Column("assign_date").CustomType<NullableDateTimeasUnixTimeUserType>().Nullable();
            Map(o => o.CreateDate).Column("submit_date").CustomType<UnixTimeUserType>().Nullable();
            Map(o => o.DeletedDate).Column("deleted").CustomType<UnixTimeUserType>().Nullable();
            Map(o => o.CompletionDate).Column("completion_date").CustomType<NullableDateTimeasUnixTimeUserType>().Nullable();
            Map(o => o.DeadlineDate).Column("deadline_date").CustomType<UnixTimeUserType>().Nullable();
            Map(o => o.DeadlineHours).Column("deadline_days");
            Map(o => o.NumSources);
            Map(o => o.StudyLevel).Column("level");
            Map(o => o.AssignmentType).Column("assignment_type");
            Map(o => o.Subject);
            Map(o => o.Topic);
            Map(o => o.Description);
            Map(o => o.CitationStyle).Column("citation_style");
            Map(o => o.English);
            Map(o => o.DiscountCode).Column("discount_code");
            Map(o => o.PrefferedWriter).Column("pref_writer");
            Map(o => o.NeedPlagiarismReport).Column("plagiarism");
            Map(o => o.TotalCost).Column("total");
            Map(o => o.TotalWithDiscount).Column("total_with_discount");
            Map(o => o.PagePrice).Column("page_price").CustomSqlType("float").Precision(15).Scale(15);
            Map(o => o.WriterPagePrice).Column("writer_page_price");
            Map(o => o.Currency);
            Map(o => o.NumPages);
            Map(o => o.StatusRaw).Column("status");
            Map(o => o.PaymentStatus).Column("payment_status");
            Map(o => o.PaymentTrackingNumber).Column("payment_tracking_number");
            Map(o => o.Revised);
            Map(o => o.DiscountTotal).Column("discount_total");
            Map(o => o.WriterBonus).Column("writer_bonus");
            Map(o => o.WriterDeadlineDate).Column("writer_deadline_date").CustomType<NullableDateTimeasUnixTimeUserType>().Nullable();
            Map(o => o.IsSurveyFilled).Column("surveyed");
            Map(o => o.NotifiedLate).Column("notified_late");
            Map(o => o.RevCount);
            Map(o => o.AvailableDate).Column("avail_date").CustomType<NullableDateTimeasUnixTimeUserType>().Nullable();
            Map(o => o.LastRevisionDate).Column("revision_date").CustomType<NullableDateTimeasUnixTimeUserType>().Nullable();
            Map(o => o.DeadlineHistory).Column("dd_history");
            Map(o => o.Spacing);
            Map(o => o.NativeLanguage).Column("native");
            Map(o => o.Outline);
            References(o => o.Major2).Column("major2").NotFound.Ignore();
            Map(o => o.DeadlineFixed).Column("deadline_fixed");
            Map(o => o.PaymentDate).Column("payment_date").CustomType<NullableDateTimeasUnixTimeUserType>().Nullable();
            Map(o => o.PaymentChargeback).Column("payment_chargeback");
            ////Map(o => o.Risk);
            Map(o => o.WasWPPRaisedDueHighRisk).Column("was_wpp_raised_due_high_risk");
            Map(o => o.Version);
            ////Map(o => o.Par)
            Map(o => o.NumberOfSlides).Column("number_of_slides");
            Map(o => o.WasOpenForEditBefore).Column("was_open_for_edit_before");
            Map(o => o.Abstract);
            References(o => o.AssignedBy).Column("assigned_by").Nullable();
            Map(o => o.CanExtendDeadline).Column("can_extend_deadline");
            HasMany(x => x.AppliedDiscounts).Inverse().Cascade.All();     // Tells NH to cascade events
            HasMany(x => x.OrderPayments).Inverse().Cascade.All(); 
        }
    }
}
