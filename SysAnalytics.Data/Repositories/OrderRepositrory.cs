using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IOrderRepository : IRepository<Order>
    {
    }
}
