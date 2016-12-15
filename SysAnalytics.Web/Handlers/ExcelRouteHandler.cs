using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SysAnalytics.Web.Handlers
{
    public class ExcelRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new ExportGridToExcel(requestContext);
        }
    }
}