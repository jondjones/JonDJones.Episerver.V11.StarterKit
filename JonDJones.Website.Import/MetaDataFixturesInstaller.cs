namespace JonDJones.Fixtures
{
    using EPiServer.Core;
    using JonDJones.Fixtures.Entities;
    using JonDJones.Fixtures.Fixtures.Factory;
    using JonDJones.Fixtures.Resources;
    using JonDJones.Website.Core.Pages.MetaPages;
    using JonDJones.Website.Shared.Helpers;
    using Website.Core.Pages;

    public class MetaDataFixturesInstaller
    {
        private readonly PagesFixturesFactory _pagesFixturesFactory;

        public MetaDataFixturesInstaller(PagesFixturesFactory pagesFixturesFactory)
        {
            Guard.ValidateObject(pagesFixturesFactory);
            _pagesFixturesFactory = pagesFixturesFactory;
        }

        public MetadataContainerReferences EnsureEssentialContainerPagesCreatedFirst(StartPage homepage)
        {
            // Main Container
            var mainContainerPage = _pagesFixturesFactory.ContainerPageFixtures()
                    .CreatePage<MetaContainerPage>(
                        FixtureConstants.PageNames.MetaContainerPage,
                        ContentReference.RootPage);
            // Settings
            var settingsPage = _pagesFixturesFactory.SettingsPageFixtures()
                .CreateSiteSettingsPage(
                    FixtureConstants.PageNames.SiteSettingsPageName,
                    mainContainerPage.ContentLink);

            var metadataContainerReferences = new MetadataContainerReferences(settingsPage,  mainContainerPage);
            return metadataContainerReferences;
        }
    }
}
