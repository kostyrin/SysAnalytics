using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using SysAnalytics.Model;
using FluentNHibernate.Mapping;
using SysAnalytics.Model.Enums;
using SysAnalytics.Model.UserTypes;

namespace SysAnalytics.Data.Mappings
{
    public class UserMap: ClassMap<User>
    {
        public UserMap()
        {
            //DiscriminateSubClassesOnColumn("UserType");
            Id(x => x.UserId).Column("id").GeneratedBy.Identity();
            Map(x => x.IsActive).Column("is_active").Not.Nullable();
            Map(x => x.RegDate).CustomType<UnixTimeUserType>().Not.Nullable();
            Map(x => x.Country).Length(2).Not.Nullable();
            //Map(x => x.Email).Length(256).Unique().Not.Nullable();
            //Map(x => x.FirstName).Length(40).Not.Nullable();
            Map(x => x.LastLogin).CustomType<UnixTimeUserType>().Nullable();
            //Map(x => x.LastName).Length(40).Not.Nullable();
            Map(x => x.TimeZone);
            //Map(x => x.PaymentMethod).Column("payment_method");
            Map(x => x.PaymentDetails).Column("payment_details");
            Map(x => x.DisableNotifications).Column("disable_notifications");
            References(x => x.Site).Column("site").NotFound.Ignore();
            Map(x => x.FindUs);
            Map(x => x.UserType);
            Map(x => x.IsProfileEditing).Column("is_profile_editing");
            Map(x => x.BirthDate).Column("birth_date").CustomType<DateTimeUserType>();
            Map(x => x.IsFrozen).Column("is_frozen");

            //
            //DiscriminateSubClassesOnColumn("type");

        }
    }
}
