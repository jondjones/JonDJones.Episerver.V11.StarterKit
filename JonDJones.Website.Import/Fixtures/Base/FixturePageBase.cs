namespace JonDJones.Fixtures.Fixtures.Base
{
    using EPiServer.Core;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class FixturePageBase : FixtureBase
    {
        public FixturePageBase(IWebsiteDependencies _websiteDependencies, IPageTypeServices pagetypeServices, IContent homepage)
            : base(_websiteDependencies, pagetypeServices)
        {
            Guard.ValidateObject(homepage);
            HomepageReference = homepage;
        }

        public IContent HomepageReference { get; }
    }
}
