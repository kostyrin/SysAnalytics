using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model;
using NHibernate;

namespace SysAnalytics.Data.Repositories
{
    public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IExpenseRepository : IRepository<Expense>
    {
    }
}
