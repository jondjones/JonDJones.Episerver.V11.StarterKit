namespace JonDJones.Fixtures.Fixtures.Pages
{
    using System;
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using JonDJones.Fixtures.Fixtures.Base;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class GenericPageFixtures : FixturePageBase
    {
        public GenericPageFixtures(IWebsiteDependencies _websiteDependencies, IPageTypeServices pagetypeServices, IContent homepage)
            : base(_websiteDependencies, pagetypeServices, homepage)
        {
        }

        public void AddContentToMainContentArea(DefaultContentPageBase item, IContent block)
        {
            item.MainContentArea = new ContentArea
            {
                Items = { new ContentAreaItem { ContentLink = block.ContentLink } }
            };

            WebsiteDependencies.ContentRepository.Save(item, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public void AddContentToMainContentArea(DefaultContentPageBase item, ContentArea contentArea)
        {
            item.MainContentArea = contentArea;
            WebsiteDependencies.ContentRepository.Save(item, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public T CreatePageWithContentArea<T>(string pageName, ContentReference parentPageReference, ContentArea mainContentArea) where T : DefaultContentPageBase
        {
            Guard.ValidateObject(parentPageReference);

            var existingPages = WebsiteDependencies.ContentRepository.GetChildren<T>(parentPageReference).ToList();

            if (existingPages.Any(x => x.Name == pageName))
            {
                return existingPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = WebsiteDependencies.ContentRepository.GetDefault<T>(parentPageReference);

            newPage.Name = pageName;
            newPage.Property["SeoTitle"].Value = pageName;
            newPage.Property["Keywords"].Value = pageName;
            newPage.Property["Description"].Value = new XhtmlString(pageName);

            if (mainContentArea != null)
            {
                newPage.MainContentArea = mainContentArea;
            }

            WebsiteDependencies.ContentRepository.Save(newPage, SaveAction.Publish, AccessLevel.NoAccess);

            return newPage;
        }

        public T CreatePage<T>(string pageName, ContentReference parentPageReference) where T : IContent
        {
            Guard.ValidateObject(parentPageReference);

            var existingPages = WebsiteDependencies.ContentRepository.GetChildren<T>(parentPageReference).ToList();

            if (existingPages.Any(x => x.Name == pageName))
            {
                return existingPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = WebsiteDependencies.ContentRepository.GetDefault<T>(parentPageReference);

            newPage.Name = pageName;
            newPage.Property["SeoTitle"].Value = pageName;
            newPage.Property["Keywords"].Value = pageName;
            newPage.Property["Description"].Value = new XhtmlString(pageName);

            WebsiteDependencies.ContentRepository.Save(newPage, SaveAction.Publish, AccessLevel.NoAccess);

            return newPage;
        }
    }
}