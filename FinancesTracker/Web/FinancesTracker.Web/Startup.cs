[assembly: Microsoft.Owin.OwinStartup(typeof(FinancesTracker.Web.Startup))]

namespace FinancesTracker.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
