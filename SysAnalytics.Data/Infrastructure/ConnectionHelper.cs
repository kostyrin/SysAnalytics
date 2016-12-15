using System;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using SysAnalytics.Data.Mappings;
using SysAnalytics.Data.Conventions;

namespace SysAnalytics.Data.Infrastructure
{
    static public class ConnectionHelper
    {
        public static ISessionFactory BuildSessionFactory(string connString)
        {
            return GetConfiguration(connString).BuildSessionFactory();
        }

        public static FluentConfiguration GetConfiguration(string connString)
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey(connString)))
                .ExposeConfiguration(c => c.SetProperty("command_timeout", (TimeSpan.FromMinutes(5).TotalSeconds).ToString()))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>()
                                .Conventions.AddFromAssemblyOf<TableNameConvention>());
        }
    }

    

    

    
}
