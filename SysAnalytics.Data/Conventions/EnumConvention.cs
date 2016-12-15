using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Utils;
using SysAnalytics.Model.UserTypes;

namespace SysAnalytics.Data.Conventions
{
    public class EnumConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        #region IPropertyConvention Members

        public void Apply(IPropertyInstance instance)
        {
            var mapperType = typeof(MySqlEnumUserType<>).MakeGenericType(instance.Property.PropertyType);
            instance.CustomType(mapperType);
        }

        #endregion

        #region IPropertyConventionAcceptance Members

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType.IsEnum ||
            (x.Property.PropertyType.IsGenericType &&
             x.Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
             x.Property.PropertyType.GetGenericArguments()[0].IsEnum)
            );
            //criteria.Expect(x => !x.Columns.HasUserDefined());
        }

        #endregion
    }
}
