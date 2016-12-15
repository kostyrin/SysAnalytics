using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SysAnalytics.Web.Core.ActionFilters
{
    public class SysAnalyticsAuthorize : AuthorizeAttribute
    {
        public SysAnalyticsAuthorize(params string[] roles)
        {
            this.Roles = String.Join(", ", roles);
        }
    }
}
