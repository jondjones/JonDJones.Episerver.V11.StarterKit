namespace JonDJones.Website.Core.EpiserverConfiguration.Admin
{
    using System.Collections.Generic;

    using EPiServer;
    using EPiServer.Security;
    using EPiServer.Shell.Navigation;

    [MenuProvider]
    public class MenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var mainAdminMenu = new SectionMenuItem("Admin", "/global/admin")
            {
                IsAvailable = request => PrincipalInfo.Current.HasPathAccess(UriSupport.Combine("/AdminPage", string.Empty))
            };

            var firstMenuItem = new UrlMenuItem("Hangfire", "/global/admin/main", "/episerver/backoffice/Plugins/hangfire")
            {
                IsAvailable = request => true,
                SortIndex = 100
            };

            var secondMenuItem = new UrlMenuItem("Logging", "/global/admin/logging", "/LogManager")
            {
                IsAvailable = request => true,
                SortIndex = 100
            };

            var thirdMenuItem = new UrlMenuItem("Elmah Logs", "/global/admin/elmah", "/elmah")
            {
                IsAvailable = request => true,
                SortIndex = 100
            };

            return new MenuItem[]
            {
                mainAdminMenu,
                firstMenuItem,
                secondMenuItem,
                thirdMenuItem
            };
        }
    }
}