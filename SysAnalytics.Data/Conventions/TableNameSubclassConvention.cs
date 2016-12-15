using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SysAnalytics.Data.Conventions
{
    public class TableNameSubclassConvention : IJoinedSubclassConvention
    {
        public void Apply(IJoinedSubclassInstance instance)
        {
            string typeName = instance.EntityType.Name;

            instance.Table(Inflector.Inflector.Pluralize(typeName));
        }
    }
}
