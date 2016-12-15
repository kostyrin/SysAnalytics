using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SysAnalytics.Web.Startup))]
namespace SysAnalytics.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
