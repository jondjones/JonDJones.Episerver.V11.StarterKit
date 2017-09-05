namespace TSC.Fixtures.Fixtures.Pages
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using Factory;
    using Resources;
    using TSC.Fixtures.Fixtures.Base;
    using TSC.Fixtures.Helpers;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.Pages;
    using TSC.Website.Core.Pages.MetaPages;
    using TSC.Website.Interfaces;
    using Website.Core.Blocks;
    using Website.Core.EpiserverConfiguration;
    using Website.Core.Pages.MetaPages.Authors;
    using Website.Core.Pages.MetaPages.Tags;
    using Website.Shared.Helpers;
    using Website.Shared.Resources;
    #endregion

    public class NewsItemPageFixtures : FixtureBase
    {
        private readonly BlockFixturesFactory blockFixturesFactory;

        private readonly PagesFixturesFactory pagesFixturesFactoryg;

        public NewsItemPageFixtures(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            BlockFixturesFactory blockFixturesFactory)
            : base(websiteDependencies, episerverContentRepositories)
        {
            Guard.ValidateObject(blockFixturesFactory);
            this.blockFixturesFactory = blockFixturesFactory;
        }

        public NewsItemPage CreateNewsPage(
            string pageName,
            ContentReference parentPageReference,
            ContentArea headerContentArea = null,
            ContentArea mainContentArea = null,
            XhtmlString mainContent = null,
            ContentReference authorsContentReference = null,
            ContentReference tagsContentReference = null,
            string comment = null)
        {
            var newsItemPages =
                this.EpiserverContentRepositories.NewsItemPageRepository.GetChildren(parentPageReference);

            if (newsItemPages.Any(x => x.Name == pageName))
            {
                return newsItemPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = this.EpiserverContentRepositories.NewsItemPageRepository.CreateNewEmptyPage(parentPageReference);

            newPage.Name = pageName;
            newPage.PageTitle = pageName;
            newPage.SeoTitle = pageName;
            newPage.Keywords = pageName;
            newPage.Description = new XhtmlString(pageName);

            if (headerContentArea != null)
            {
                newPage.HeaderContentArea = headerContentArea;
            }

            if (mainContentArea != null)
            {
                newPage.MainContentArea = mainContentArea;
            }

            if (mainContent != null)
            {
                newPage.MainContent = mainContent;
            }

            if (ContentReference.IsNullOrEmpty(authorsContentReference))
            {
                newPage.PageAuthor = authorsContentReference.ID.ToString();
            }

            if (ContentReference.IsNullOrEmpty(tagsContentReference))
            {
                newPage.Tags = tagsContentReference.ID.ToString();
            }

            if (!string.IsNullOrEmpty(comment))
            {
                newPage.Comment = comment;
            }

            var reference = this.EpiserverContentRepositories.NewsItemPageRepository.Save(newPage);

            return reference != null ? newPage : null;
        }

        public ContentReference CreateCarouselBlock(IEnumerable<ContentReference> contentReferences)
        {
            var listContent = contentReferences.Select(contentReference => this.WebsiteDependencies.ContentRepository.Get<IContent>(contentReference)).ToList();
            return blockFixturesFactory.CarouselFixtures().CreateCarousel(ContentReference.GlobalBlockFolder, FixtureConstants.PageNames.CarouselContainer, listContent);
        }

        public ContentReference CreateCarouselSlideBlock(string carouselSlideName, int imageSelector)
        {
            return blockFixturesFactory.CarouselSlideFixtures().CreateDummyCarouselSlide(ContentReference.GlobalBlockFolder, carouselSlideName, imageSelector);
        }

        public ContentArea CreateHeaderContentArea(IEnumerable<ContentReference> contentReferences)
        {
            var contentArea = new ContentArea();

            foreach (var contentReference in contentReferences)
            {
                var carouselContent = this.WebsiteDependencies.ContentRepository.Get<IContent>(contentReference);
                contentArea.Items.Add(new ContentAreaItem { ContentLink = carouselContent.ContentLink });
            }

            return contentArea;
        }

        public XhtmlString CreateMainContent(string newsMainContent)
        {
            return new XhtmlString(newsMainContent);
        }

        public ContentArea CreateMainContentArea(string newsMainContentArea)
        {
            var richTextBlock = this.blockFixturesFactory.RichTextFixtures().CreateRichTextBlock(ContentReference.GlobalBlockFolder, newsMainContentArea, "LoremIpsum");
            return new ContentArea
            {
                Items =
                {
                    new ContentAreaItem
                    {
                        ContentLink = richTextBlock
                    }
                }
            };
        }

        public NewsItemPage CreateDummyNewsPage(ContentReference parentPage, AuthorsContainerPage authorsContainerPage, TagsContainerPage tagsContainerPage)
        {
            AuthorsPage authorsPage = null;
            TagsPage tagsPage = null;
            var carouselBlockList = new List<ContentReference>();
            var carouselSlideBlockList =
            new List<ContentReference>
                {
                        CreateCarouselSlideBlock(FixtureConstants.PageNames.CarouselSlide1, 1),
                        CreateCarouselSlideBlock(FixtureConstants.PageNames.CarouselSlide2, 2),
                        CreateCarouselSlideBlock(FixtureConstants.PageNames.CarouselSlide3, 3),
                        CreateCarouselSlideBlock(FixtureConstants.PageNames.CarouselSlide4, 4)
                };

            var carouselBlock = CreateCarouselBlock(carouselSlideBlockList);

            carouselBlockList.Add(carouselBlock);

            var headerContentArea = CreateHeaderContentArea(carouselBlockList);

            var mainContentArea = CreateMainContentArea(FixtureConstants.NewsConfig.NewsMainContentArea);

            var mainContent = CreateMainContent(FixtureConstants.NewsConfig.NewsMainContent);

            return CreateNewsPage(
                FixtureConstants.PageNames.NewsPageName,
                parentPage,
                headerContentArea,
                mainContentArea,
                mainContent,
                authorsContainerPage.ContentLink,
                tagsContainerPage.ContentLink,
                FixtureConstants.NewsConfig.NewsComment);
        }
    }
}