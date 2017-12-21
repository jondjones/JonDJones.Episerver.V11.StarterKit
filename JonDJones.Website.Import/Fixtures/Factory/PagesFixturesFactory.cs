using EPiServer.Core;

using JonDJones.Fixtures.Fixtures.Blocks;
using JonDJones.Fixtures.Fixtures.Pages;
using JonDJones.Fixtures.Helpers;
using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Fixtures.Fixtures.Factory
{
    public class PagesFixturesFactory
    {
        private readonly IWebsiteDependencies _websiteDependencies;

        private readonly IPageTypeServices _pagetypeServices;

        private readonly IContent _homepage;

        private readonly BlockFixturesFactory _blockFixturesFactory;

        private readonly ContentHelper _contentHelper;

        public PagesFixturesFactory(
            IWebsiteDependencies websiteDependencies,
            IPageTypeServices episerverContentRepositories,
            StartPage homepage,
            ContentHelper contentHelper,
            BlockFixturesFactory blockFixturesFactory)
        {
            Guard.ValidateObject(websiteDependencies);
            Guard.ValidateObject(homepage);
            Guard.ValidateObject(contentHelper);
            Guard.ValidateObject(episerverContentRepositories);

            _websiteDependencies = websiteDependencies;
            _homepage = homepage;
            _blockFixturesFactory = blockFixturesFactory;
            _pagetypeServices = episerverContentRepositories;
        }

        public HomePageFixtures HomepageFixtures()
        {
            return new HomePageFixtures(_websiteDependencies, _pagetypeServices, _blockFixturesFactory, _contentHelper);
        }

        public GenericPageFixtures GenericPagesFixtures()
        {
            return new GenericPageFixtures(_websiteDependencies, _pagetypeServices, _homepage);
        }

        public ContainerPageFixtures ContainerPageFixtures()
        {
            return new ContainerPageFixtures(_websiteDependencies, _pagetypeServices, _homepage);
        }

        public KeyValuePageFixtures KeyValuePageFixtures()
        {
            return new KeyValuePageFixtures(_websiteDependencies, _pagetypeServices, _homepage);
        }

        public SettingsPageFixtures SettingsPageFixtures()
        {
            return new SettingsPageFixtures(_websiteDependencies, _pagetypeServices, _homepage);
        }

        public MenuPageFixtures MenuPageFixtures()
        {
            return new MenuPageFixtures(_websiteDependencies, _pagetypeServices, _homepage);
        }

        public RichtextFixtures RichTextFixtures()
        {
            return new RichtextFixtures(_websiteDependencies, _pagetypeServices);
        }
    }
}