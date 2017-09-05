namespace TSC.Fixtures.Fixtures.Pages
{
    #region Using

    using System.Linq;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Interfaces;
    using TSC.Website.Shared.Helpers;
    using Website.Core.Pages.MetaPages.Authors;
    using Website.Core.Pages.MetaPages.Tags;

    #endregion

    public class TagsPageFixtures : FixturePageBase
    {
        #region Constructors and Destructors

        public TagsPageFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories, IContent homepage)
            : base(websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        #endregion

        #region Public Methods

        public TagsPage CreatePage(
            string pageName,
            ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);

            var tagPage = this.EpiserverContentRepositories.TagsRepository.GetChildren(parentPageReference);

            if (tagPage.Any(x => x.Name == pageName))
            {
                return tagPage.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = this.EpiserverContentRepositories.TagsRepository.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            newPage.TagName = pageName;

            this.EpiserverContentRepositories.TagsRepository.Save(newPage);

            return newPage;
        }

        #endregion
    }
}