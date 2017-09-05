namespace JonDJones.Fixtures
{
    using System;
    using System.Web.Security;

    using EPiServer.ServiceLocation;
    using EPiServer.Web;
    using Fixtures;
    using Fixtures.Factory;
    using Fixtures.Pages;
    using Helpers;
    using log4net;
    using Resources;

    using JonDJones.Fixtures.Entities;

    using Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using Website.Core.Pages;
    using Website.Core.Pages.MetaPages.Menu;
    using Website.Interfaces;
    using Website.Shared.Helpers;

    public class FixturesInstaller
    {
        private readonly BlockFixturesFactory _blockFixturesFactory;

        private readonly ContentHelper _contentHelper;

        private readonly StartPage _homepage;

        private readonly ILog log = LogManager.GetLogger(typeof(FixturesInstaller));

        private readonly PagesFixturesFactory _pagesFixturesFactory;

        private readonly HomePageFixtures _homepageFixtures;

        private readonly MetaDataFixturesInstaller _metaDataFixturesInstaller;

        private readonly GenericPageFixtures _genericPageFixtures;

        private MetadataContainerReferences _metadataContainerReferences;

        public FixturesInstaller(IWebsiteDependencies dependencies, IEpiserverContentRepositories episerverContentRepositories)
        {
            Guard.ValidateObject(dependencies);
            Guard.ValidateObject(episerverContentRepositories);

            _blockFixturesFactory = new BlockFixturesFactory(dependencies, episerverContentRepositories);
            _contentHelper = new ContentHelper(_blockFixturesFactory);

            _homepageFixtures = new HomePageFixtures(dependencies, episerverContentRepositories, _blockFixturesFactory, _contentHelper);
            _homepage = SetupInitialHomepage(dependencies, episerverContentRepositories);

            _pagesFixturesFactory = new PagesFixturesFactory(
                dependencies,
                episerverContentRepositories,
                _homepage,
                _contentHelper,
                _blockFixturesFactory);

            _metaDataFixturesInstaller = new MetaDataFixturesInstaller(_pagesFixturesFactory);
            _genericPageFixtures = _pagesFixturesFactory.GenericPagesFixtures();

            Guard.ValidateObject(_genericPageFixtures);
            Guard.ValidateObject(_metaDataFixturesInstaller);
            Guard.ValidateObject(_pagesFixturesFactory);
            Guard.ValidateObject(_homepageFixtures);
        }

        public void SetupWebsite()
        {
            ConfigureEpiserver();
            var metadataContainerReferences = CreateMetaDataPages();
            CreateContentPages(metadataContainerReferences);
            CreateDefaultUser();
        }

        private StartPage SetupInitialHomepage(
            IWebsiteDependencies dependencies,
            IEpiserverContentRepositories episerverContentRepositories)
        {
            var tempHomepage = _homepageFixtures.GetOrCreateBlankHomePage(FixtureConstants.PageNames.HomePage);

            var tempPagesFixturesFactory = new PagesFixturesFactory(
                dependencies,
                episerverContentRepositories,
                tempHomepage,
                _contentHelper,
                _blockFixturesFactory);

            var tempMetaDataFixturesInstaller = new MetaDataFixturesInstaller(tempPagesFixturesFactory);
            _metadataContainerReferences = tempMetaDataFixturesInstaller.EnsureEssentialContainerPagesCreatedFirst(tempHomepage);
            return _homepageFixtures.EnsureSettingsAndResourcePagesExist(tempHomepage, _metadataContainerReferences);
        }

        private MetadataContainerReferences CreateMetaDataPages()
        {
            var menuContainerPage =
                _pagesFixturesFactory.ContainerPageFixtures()
                    .CreatePage<MenuContainerPage>(
                        FixtureConstants.PageNames.MenuContainer,
                        _metadataContainerReferences.MetaContainerPage.ContentLink);

            Guard.ValidateObject(menuContainerPage);
            _metadataContainerReferences.MenuContainerPage = menuContainerPage;
            return _metadataContainerReferences;
        }

        private void CreateContentPages(MetadataContainerReferences metaDataReferences)
        {
            Guard.ValidateObject(metaDataReferences);

            // Search
            var searchPage = _genericPageFixtures.CreatePage<SearchPage>(
                FixtureConstants.PageNames.SearchPage,
                _homepage.ContentLink);

            // Configure Search Page
            _pagesFixturesFactory.SettingsPageFixtures()
                .UpdateSettingsPage(_metadataContainerReferences, searchPage.PageLink);

            // 404
            _genericPageFixtures.CreatePage<PageNotFoundPage>(FixtureConstants.PageNames.PageNotFound, _homepage.ContentLink);

            // Content Pages
            var genericPage =
                _pagesFixturesFactory.GenericPagesFixtures().CreatePage<ContentPage>("Dummy Page 1", _homepage.ContentLink);

            _pagesFixturesFactory.MenuPageFixtures()
                .CreateDummyMenuPages(
                metaDataReferences.MenuContainerPage,
                genericPage);

            _homepageFixtures.PopulateHomePage(_homepage, metaDataReferences);
        }

        private void ConfigureEpiserver()
        {
            var siteDefinitionRepository = ServiceLocator.Current.GetInstance<ISiteDefinitionRepository>();
            EpiserverConfigurationSetup.CreateSite(siteDefinitionRepository, _homepage, "http://jondjones.local");
        }

        private void CreateDefaultUser()
        {
            try
            {
                Roles.CreateRole("Administrators");
            }
            catch (Exception exception)
            {
                log.Error(exception);
            }

            try
            {
                var user = Membership.CreateUser("episerver", "episerver", "episerver@episerver.com");
                user.IsApproved = true;
            }
            catch (Exception exception)
            {
                log.Error(exception);
            }

            try
            {
                Roles.AddUserToRole("episerver", "Administrators");
            }
            catch (Exception exception)
            {
                log.Error(exception);
            }
        }
    }
}