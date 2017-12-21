namespace JonDJones.Fixtures.Fixtures.Factory
{
    using JonDJones.Fixtures.Fixtures.Blocks;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class BlockFixturesFactory
    {
        private readonly IWebsiteDependencies _websiteDependencies;

        private readonly IPageTypeServices _pagetypeServices;

        public BlockFixturesFactory(IWebsiteDependencies websiteDependencies, IPageTypeServices pagetypeServices)
        {
            Guard.ValidateObject(websiteDependencies);
            Guard.ValidateObject(pagetypeServices);

            _websiteDependencies = websiteDependencies;
            _pagetypeServices = pagetypeServices;
        }

        public RichtextFixtures RichTextFixtures()
        {
            return new RichtextFixtures(_websiteDependencies, _pagetypeServices);
        }
    }
}