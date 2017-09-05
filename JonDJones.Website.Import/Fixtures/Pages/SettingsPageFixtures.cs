namespace JonDJones.Fixtures.Fixtures.Pages
{
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using JonDJones.Fixtures.Entities;
    using JonDJones.Fixtures.Fixtures.Base;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class SettingsPageFixtures : FixturePageBase
    {
        public SettingsPageFixtures(IWebsiteDependencies _websiteDependencies, IEpiserverContentRepositories episerverContentRepositories, IContent homepage)
            : base(_websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        public SiteSettingsPage CreateSiteSettingsPage(
            string pageName,
            ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);

            var existingSiteSettingsPage =
                WebsiteDependencies.ContentRepository.GetChildren<SiteSettingsPage>(parentPageReference)
                    .ToList();

            if (existingSiteSettingsPage.Any(x => x.Name == pageName))
            {
                return existingSiteSettingsPage.FirstOrDefault(x => x.PageName == pageName);
            }

            var siteSettingsPage =
                WebsiteDependencies.ContentRepository.GetDefault<SiteSettingsPage>(parentPageReference);

            siteSettingsPage.Name = pageName;

            var reference = WebsiteDependencies.ContentRepository.Save(
                siteSettingsPage,
                SaveAction.Publish,
                AccessLevel.NoAccess);
            return reference != null ? siteSettingsPage : null;
        }

        public SiteSettingsPage UpdateSettingsPage(
            MetadataContainerReferences metadataContainerReferences,
            PageReference searchPageReference)
        {
            var clone = metadataContainerReferences.SettingsPage.CreateWritableClone() as SiteSettingsPage;

            clone.SearchPage = searchPageReference;

            var reference = WebsiteDependencies.ContentRepository.Save(
                clone,
                SaveAction.Publish,
                AccessLevel.NoAccess);

            return reference != null ? clone : null;
        }
    }
}