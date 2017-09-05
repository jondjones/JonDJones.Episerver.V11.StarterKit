namespace JonDJones.Website.Core.EpiserverConfiguration.Filters
{
    using Hangfire.Annotations;
    using Hangfire.Dashboard;

    public class EpiserverAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return EPiServer.Security.PrincipalInfo.HasAdminAccess;
        }
    }
}
