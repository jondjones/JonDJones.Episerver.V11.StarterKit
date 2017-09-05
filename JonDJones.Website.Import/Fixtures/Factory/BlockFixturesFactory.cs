namespace JonDJones.Fixtures.Fixtures.Factory
{
    using JonDJones.Fixtures.Fixtures.Blocks;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class BlockFixturesFactory
    {
        private readonly IWebsiteDependencies _websiteDependencies;

        private readonly IEpiserverContentRepositories _episerverContentRepositories;

        public BlockFixturesFactory(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
        {
            Guard.ValidateObject(websiteDependencies);
            Guard.ValidateObject(episerverContentRepositories);
            _websiteDependencies = websiteDependencies;
            _episerverContentRepositories = episerverContentRepositories;
        }

        public RichtextFixtures RichTextFixtures()
        {
            return new RichtextFixtures(_websiteDependencies, _episerverContentRepositories);
        }
    }
}