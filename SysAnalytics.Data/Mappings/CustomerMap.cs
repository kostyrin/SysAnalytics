using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using SysAnalytics.Model;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Mappings
{
    public class CustomerMap : SubclassMap<Customer>
    {
        public CustomerMap()
        {
            Table("customers");
            Map(c => c.CustomerId).Column("user_id");
            Map(c => c.Employed);
            Map(c => c.MainMajors).Column("major");
            Map(c => c.StudyLevel).Column("study_level");
            Map(c => c.CurrentGPA).Column("current_gpa");
            Map(c => c.DesiredGPA).Column("desired_gpa");
            Map(c => c.EnglishNative).Column("english_native");
            Map(c => c.EnglishStudyYears).Column("english_study_years");
            Map(c => c.Difficulties);
            Map(c => c.CommonMistakes).Column("common_mistakes");
            Map(c => c.CustomerQualityExpectations).Column("quality_expectations");
            Map(c => c.AdditionalComments).Column("additional_comments");
            Map(c => c.NumCompletedOrders).Column("num_completed_orders");
            Map(c => c.NumCompletedPages).Column("num_completed_pages");
            Map(c => c.SendTips).Column("tips");
            Map(c => c.SendSeasonal).Column("seasonal");
            Map(c => c.IsWroteReview).Column("is_wrote_review");
            Map(c => c.Emergency);
            References(c => c.Tempter).Column("tempter_id");
            Map(c => c.Bonus);
            Map(c => c.IsPartner).Column("is_partner");
            Map(c => c.BalancePages).Column("balance_pages");
            Map(c => c.IsSubscriber).Column("subscriber");
            Map(c => c.DeniedCustomer).Column("is_denied_customer");
            Map(c => c.City);
            Map(c => c.Degree);
            Map(c => c.CountryStudy).Column("country_study");
            Map(c => c.EnglishLevel).Column("english_level");
        }
    }
}
