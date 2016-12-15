using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model;
using NHibernate;

namespace SysAnalytics.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
