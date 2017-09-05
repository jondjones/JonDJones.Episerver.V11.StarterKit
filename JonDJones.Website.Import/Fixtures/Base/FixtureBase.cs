namespace JonDJones.Fixtures.Fixtures.Base
{
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class FixtureBase
    {
        public FixtureBase(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
        {
            Guard.ValidateObject(websiteDependencies);
            Guard.ValidateObject(episerverContentRepositories);
            WebsiteDependencies = websiteDependencies;
            EpiserverContentRepositories = episerverContentRepositories;
        }

        public IWebsiteDependencies WebsiteDependencies { get; }

        public IEpiserverContentRepositories EpiserverContentRepositories { get; }
    }
}
