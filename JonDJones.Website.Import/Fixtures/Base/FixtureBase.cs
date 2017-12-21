using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Fixtures.Fixtures.Base
{
    public class FixtureBase
    {
        public FixtureBase(IWebsiteDependencies websiteDependencies, IPageTypeServices pagetypeServices)
        {
            Guard.ValidateObject(websiteDependencies);
            Guard.ValidateObject(pagetypeServices);

            WebsiteDependencies = websiteDependencies;
            PageTypeServices = pagetypeServices;
        }

        public IWebsiteDependencies WebsiteDependencies { get; }

        public IPageTypeServices PageTypeServices { get; }
    }
}
