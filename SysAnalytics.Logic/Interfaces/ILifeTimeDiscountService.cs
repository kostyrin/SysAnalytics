using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Logic.Interfaces
{
    public interface ILifeTimeDiscountService
    {
        IList<LifeTimeDiscount> GetLifetimeDiscountSettings();
        LifetimeDiscountInfo ForUser(int numCompletedPages);
    }
}
