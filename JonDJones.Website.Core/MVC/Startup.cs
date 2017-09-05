using Microsoft.Owin;

using JonDJones.Website.Core.MVC;

[assembly: OwinStartup(typeof(Startup))]

namespace JonDJones.Website.Core.MVC
{
    using System.Configuration;

    using Hangfire;

    using Owin;

    using JonDJones.Website.Core.EpiserverConfiguration.Filters;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var connection = ConfigurationManager.ConnectionStrings["EPiServerDB"].ConnectionString;
            GlobalConfiguration.Configuration.UseSqlServerStorage(connection);

            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    new EpiserverAuthorizationFilter()
                }
            };

            app.UseHangfireDashboard("/episerver/backoffice/Plugins/hangfire", dashboardOptions);
            app.UseHangfireServer();
        }
    }
}