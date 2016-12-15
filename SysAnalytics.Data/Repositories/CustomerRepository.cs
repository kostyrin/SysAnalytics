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
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
