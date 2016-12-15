using NHibernate;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Repositories
{
    public class EmailingRepository : RepositoryBase<Emailing>, IEmailingRepository
    {
        public EmailingRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IEmailingRepository : IRepository<Emailing>
    {
    }
}
