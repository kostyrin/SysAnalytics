using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SysAnalytics.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
