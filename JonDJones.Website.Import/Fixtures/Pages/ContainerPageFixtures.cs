namespace JonDJones.Fixtures.Fixtures.Pages
{
    using System;
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using JonDJones.Fixtures.Fixtures.Base;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class ContainerPageFixtures : FixturePageBase
    {
        public ContainerPageFixtures(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            IContent homepage)
            : base(websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        public T CreatePage<T>(
            string pageName,
            ContentReference parentPageReference) where T : IContent
        {
            Guard.ValidateObject(parentPageReference);

            var pages = WebsiteDependencies.ContentRepository.GetChildren<T>(parentPageReference).ToList();

            if (pages.Any(x => x.Name == pageName))
            {
                return pages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = WebsiteDependencies.ContentRepository.GetDefault<T>(parentPageReference);
            newPage.Name = pageName;

            WebsiteDependencies.ContentRepository.Save(newPage, SaveAction.Publish, AccessLevel.NoAccess);

            return newPage;
        }
    }
}