using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysAnalytics.Web.Controllers
{
    [Authorize]
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult Index()
        {
            return View();
        }
    }
}