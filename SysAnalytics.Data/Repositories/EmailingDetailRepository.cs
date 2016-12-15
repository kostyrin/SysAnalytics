using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Repositories
{
    public class EmailingDetailRepository : RepositoryBase<EmailingDetail>, IEmailingDetailRepository
    {
        public EmailingDetailRepository(ISession session)
            : base(session)
        {
            
        }
    }

    public interface IEmailingDetailRepository : IRepository<EmailingDetail>
    {
    }
}
