using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model;
using NHibernate;

namespace SysAnalytics.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IUserRepository : IRepository<User>
    {
    }
}
