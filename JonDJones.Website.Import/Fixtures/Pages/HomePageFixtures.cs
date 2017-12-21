using System;
using System.Linq;
using System.Web.Hosting;

using EPiServer.Core;

using JonDJones.Fixtures.Entities;
using JonDJones.Fixtures.Fixtures.Base;
using JonDJones.Fixtures.Helpers;
using JonDJones.Fixtures.Resources;
using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Interfaces;
using JonDJones.Fixtures.Fixtures.Factory;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Fixtures.Fixtures.Pages
{
    public class HomePageFixtures : FixtureBase
    {
        private readonly BlockFixturesFactory _blockFixturesFactory;

        private readonly ContentHelper _contentHelper;

        public HomePageFixtures(
            IWebsiteDependencies websiteDependencies,
            IPageTypeServices pagetypeServices,
            BlockFixturesFactory blockFixturesFactory,
            ContentHelper contentHelper)
            : base(websiteDependencies, pagetypeServices)
        {
            Guard.ValidateObject(blockFixturesFactory);
            Guard.ValidateObject(contentHelper);

            _blockFixturesFactory = blockFixturesFactory;
            _contentHelper = contentHelper;
        }

        public StartPage GetOrCreateBlankHomePage(string pageName)
        {
            var existingPages =
                PageTypeServices.StartPageService.GetChildren(
                    WebsiteDependencies.ContextResolver.RootPage);

            if (existingPages != null && existingPages.Any(x => x.Name == pageName))
            {
                return existingPages.FirstOrDefault(x => x.PageName == pageName);
            }

            var newPage =
                PageTypeServices.StartPageService.CreateNewEmptyPage(
                    WebsiteDependencies.ContextResolver.RootPage);

            newPage.Name = pageName;
            newPage.PageTitle = pageName;
            newPage.SeoTitle = pageName;
            newPage.Keywords = pageName;
            newPage.Description = new XhtmlString(pageName);

            PageTypeServices.StartPageService.Save(newPage);

            return newPage;
        }

        public void UpdateHomePage(
            StartPage startPage,
            MetadataContainerReferences metadataContainerReferences,
            string footerText,
            ContentArea mainContentArea,
            PageReference menuContainerPageReference,
            ContentReference logo,
            ContentReference mobileLogo)
        {
            var homePage = startPage.CreateWritableClone() as StartPage;

            homePage.PrimaryNavigationContainerPage = metadataContainerReferences.MenuContainerPage.PageLink;
            homePage.FooterText = new XhtmlString(footerText);
            homePage.CopyRightNotice = footerText;
            homePage.MainContentArea = mainContentArea;
            homePage.Logo = logo;
            homePage.MobileLogo = mobileLogo;

            PageTypeServices.StartPageService.Save(homePage);
        }

        public StartPage EnsureSettingsAndResourcePagesExist(StartPage startPage, MetadataContainerReferences metadataContainerReferences)
        {
            if (startPage == null)
            {
                throw new InvalidOperationException();
            }

            var requireSave = PageReference.IsNullOrEmpty(startPage.SiteSettingsPage);

            if (!requireSave)
            {
                return startPage;
            }

            var homePage = startPage.CreateWritableClone() as StartPage;
            homePage.SiteSettingsPage = metadataContainerReferences.SettingsPage.PageLink;

            PageTypeServices.StartPageService.Save(homePage);

            return homePage;
        }

        public void PopulateHomePage(StartPage startPage, MetadataContainerReferences metadataContainerReferences)
        {

            var mainContentArea = CreateHomePageContentArea();

            UpdateHomePage(
                startPage,
                metadataContainerReferences,
                "I'm a Footer",
                mainContentArea,
                metadataContainerReferences.MenuContainerPage.PageLink,
                null,
                null);
        }

        public ContentReference CreateImage(string folderName, string fileName, int displayWidth = 0, int displayHeight = 0, string alternateText = "")
        {
            var contentFolder = WebsiteDependencies.AssetHandler.CreateDirectory(folderName, WebsiteDependencies.ContextResolver.SiteAssetsFolder);
            var image = WebsiteDependencies.AssetHandler.InsertMediaByPath(
                contentFolder,
                fileName,
                HostingEnvironment.MapPath(FixtureConstants.FilePath.ImagePath + fileName),
                displayWidth,
                displayHeight,
                alternateText);

            return image;
        }

        private ContentArea CreateHomePageContentArea()
        {
            var richTextBlock = _blockFixturesFactory.RichTextFixtures()
                .CreateDummyRichTextBlock();

            var contentArea = new ContentArea
            {
                Items =
                {
                    new ContentAreaItem
                    {
                        ContentLink = richTextBlock
                    }
                }
            };

            return contentArea;
        }
    }
}