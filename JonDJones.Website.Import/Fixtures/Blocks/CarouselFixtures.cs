namespace TSC.Fixtures.Fixtures.Blocks
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using Website.Core.Blocks;
    using Website.Interfaces;

    public class CarouselFixtures : FixtureBase
    {
        public CarouselFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public ContentReference CreateCarousel(
            ContentReference parent,
            string name,
            List<IContent> blocks)
        {
            var existingCarousel =
                this.WebsiteDependencies.ContentRepository.GetChildren<CarouselBlock>(parent)
                    .FirstOrDefault();

            if (existingCarousel != null)
            {
                return (existingCarousel as IContent).ContentLink;
            }

            var carousel =
                this.WebsiteDependencies.ContentRepository.GetDefault<CarouselBlock>(parent);

            var contentArea = new ContentArea();

            foreach (var block in blocks)
            {
                contentArea.Items.Add(new ContentAreaItem { ContentLink = block.ContentLink });
            }

            carousel.Slides = contentArea;

            var content = carousel as IContent;
            content.Name = name;

            return this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }
    }
}