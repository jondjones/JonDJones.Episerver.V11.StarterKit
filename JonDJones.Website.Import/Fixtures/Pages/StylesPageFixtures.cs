namespace TSC.Fixtures.Fixtures.Pages
{
    #region Using

    using System.Linq;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.Pages.MetaPages.Styles;
    using TSC.Website.Interfaces;
    using TSC.Website.Shared.Helpers;

    #endregion

    /// <summary>
    ///     Button Page Fixtures
    /// </summary>
    /// <seealso cref="FixturePageBase" />
    public class StylesPageFixtures : FixturePageBase
    {
        #region Constructors and Destructors

        public StylesPageFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories, IContent homepage)
            : base(websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates the page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="parentPageReference">The parent page reference.</param>
        /// <param name="buttonClassValue">Name of the background class.</param>
        /// <returns>
        ///     Created Page
        /// </returns>
        public StylesPage CreatePage(
            string pageName,
            ContentReference parentPageReference,
            string buttonClassValue)
        {
            Guard.ValidateObject(parentPageReference);

            var buttonPages = this.EpiserverContentRepositories.StylesRepository.GetChildren(parentPageReference);
            if (buttonPages.Any(x => x.Name == pageName))
            {
                return buttonPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = this.EpiserverContentRepositories.StylesRepository.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            newPage.ClassName = pageName;
            newPage.ClassValue = buttonClassValue;

            this.EpiserverContentRepositories.StylesRepository.Save(newPage);

            return newPage;
        }

        #endregion
    }
}