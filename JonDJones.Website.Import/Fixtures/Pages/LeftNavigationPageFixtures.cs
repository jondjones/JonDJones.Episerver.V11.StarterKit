namespace TSC.Fixtures.Fixtures.Pages
{
    using System.Linq;
    using EPiServer.Core;
    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.Pages;
    using TSC.Website.Interfaces;
    using TSC.Website.Shared.Helpers;

    public class LeftNavigationPageFixtures : FixturePageBase
    {
        public LeftNavigationPageFixtures(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            IContent homepage)
            : base(websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        public LeftNavigationPage CreatePage(
            string pageName,
            ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);

            var existingPages = this.EpiserverContentRepositories.LeftNavigationPageRepository.GetChildren(parentPageReference).ToList();

            if (existingPages.Any(x => x.Name == pageName))
            {
                return existingPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = this.EpiserverContentRepositories.LeftNavigationPageRepository.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            newPage.PageTitle = pageName;
            newPage.SeoTitle = pageName;
            newPage.Keywords = pageName;
            newPage.Description = new XhtmlString(pageName);

            this.EpiserverContentRepositories.LeftNavigationPageRepository.Save(newPage);

            return newPage;
        }
    }
}