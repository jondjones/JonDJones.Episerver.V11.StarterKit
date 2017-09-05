namespace TSC.Fixtures.Fixtures.Pages
{
    using System.Linq;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using EPiServer.Web.Routing;
    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.EpiserverConfiguration.Media;
    using TSC.Website.Core.Pages.MetaPages.Styles;
    using TSC.Website.Interfaces;
    using TSC.Website.Shared.Helpers;
    using Website.Core.Pages.MetaPages.Authors;

    public class AuthorsPageFixtures : FixturePageBase
    {
        public AuthorsPageFixtures(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            IContent homepage)
            : base(websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        public AuthorsPage CreatePage(
            string pageName,
            ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);

            var authorsPage = this.EpiserverContentRepositories.AuthorsRepository.GetChildren(parentPageReference);

            if (authorsPage.Any(x => x.Name == pageName))
            {
                return authorsPage.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = this.EpiserverContentRepositories.AuthorsRepository.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            newPage.AuthorName = pageName;
            newPage.AuthorBiography = new XhtmlString(pageName);

            //// todo (SB)
            //// newPage.AuthorPicture = this.WebsiteDependencies.AssetHandler.InsertMediaByUrl<ImageFile>(UrlResolver.Current.GetUrl("~/assets/img/profile.jpg"), ".jpg");

            this.EpiserverContentRepositories.AuthorsRepository.Save(newPage);

            return newPage;
        }
    }
}