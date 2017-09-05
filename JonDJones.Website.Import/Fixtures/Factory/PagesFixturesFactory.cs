namespace JonDJones.Fixtures.Fixtures.Factory
{
    using EPiServer.Core;

    using JonDJones.Fixtures.Fixtures.Blocks;
    using JonDJones.Fixtures.Fixtures.Pages;
    using JonDJones.Fixtures.Helpers;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class PagesFixturesFactory
    {
        private readonly IWebsiteDependencies _websiteDependencies;

        private readonly IEpiserverContentRepositories _episerverContentRepositories;

        private readonly IContent _homepage;

        private readonly BlockFixturesFactory _blockFixturesFactory;

        private readonly ContentHelper _contentHelper;

        public PagesFixturesFactory(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            IContent homepage,
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
            _episerverContentRepositories = episerverContentRepositories;
        }

        public HomePageFixtures HomepageFixtures()
        {
            return new HomePageFixtures(_websiteDependencies, _episerverContentRepositories, _blockFixturesFactory, _contentHelper);
        }

        public GenericPageFixtures GenericPagesFixtures()
        {
            return new GenericPageFixtures(_websiteDependencies, _episerverContentRepositories, _homepage);
        }

        public ContainerPageFixtures ContainerPageFixtures()
        {
            return new ContainerPageFixtures(_websiteDependencies, _episerverContentRepositories, _homepage);
        }

        public KeyValuePageFixtures KeyValuePageFixtures()
        {
            return new KeyValuePageFixtures(_websiteDependencies, _episerverContentRepositories, _homepage);
        }

        public SettingsPageFixtures SettingsPageFixtures()
        {
            return new SettingsPageFixtures(_websiteDependencies, _episerverContentRepositories, _homepage);
        }

        public MenuPageFixtures MenuPageFixtures()
        {
            return new MenuPageFixtures(_websiteDependencies, _episerverContentRepositories, _homepage);
        }

        public RichtextFixtures RichTextFixtures()
        {
            return new RichtextFixtures(_websiteDependencies, _episerverContentRepositories);
        }
    }
}