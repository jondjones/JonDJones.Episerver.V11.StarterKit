namespace TSC.Fixtures.Fixtures.Blocks
{
    using System;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Blocks;
    using TSC.Website.Interfaces;
    using Website.Core.Dependencies.RepositoryDependencies.Interfaces;

    public class CarouselSlideFixtures : FixtureBase
    {
        public CarouselSlideFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public ContentReference CreateCarouselSlide(
            ContentReference parent,
            string name,
            string title,
            string description,
            string button1,
            string button2,
            string slideDesktopBackgroundImage,
            string slideMobileBackgroundImage,
            string slideThumbnailImage,
            string thumbnailText)
        {
            var existingCarouselSlides = this.WebsiteDependencies.ContentRepository.GetChildren<CarouselSlideBlock>(parent);

            CarouselSlideBlock existingSlide = null;

            foreach (var slide in existingCarouselSlides)
            {
                var slideContent = slide as IContent;

                if (slideContent?.Name == name)
                {
                    existingSlide = slide;
                }
            }

            if (existingSlide != null)
            {
                return (existingSlide as IContent).ContentLink;
            }

            var carouselSlide = this.WebsiteDependencies.ContentRepository.GetDefault<CarouselSlideBlock>(parent);
            carouselSlide.Title = title;
            carouselSlide.Description = new XhtmlString(description);
            carouselSlide.Button1 = button1;
            carouselSlide.Button2 = button2;
            carouselSlide.SlideDesktopBackgroundImage = new Url(slideDesktopBackgroundImage);
            carouselSlide.SlideMobileBackgroundImage = new Url(slideMobileBackgroundImage);
            carouselSlide.SlideThumbnailImage = new Url(slideThumbnailImage);
            carouselSlide.ThumbnailText = thumbnailText;

            var content = carouselSlide as IContent;
            content.Name = name;

            return this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference CreateDummyCarouselSlide(
            ContentReference parent,
            string name,
            int imageSelector)
        {
            var title = $"Slide {imageSelector} alt lorem ipsum dolo";
            var description = "Heading 5 lorem ipsum dolor sit amet, consectetur adipiscing elit, do elit sit eiusmod et tempor incididunt aliqua.";
            var button1 = "Lorem ipsum dolor";
            var button2 = "Lorem ipsum dolor";
            var slideDesktopBackgroundImage = $"http://lorempixel.com/1366/445/abstract/{imageSelector}/";
            var slideMobileBackgroundImage = $"http://lorempixel.com/320/620/abstract/{imageSelector}/";
            var slideThumbnailImage = $"http://lorempixel.com/107/75/abstract/{imageSelector}/";
            var thumbnailText = $"Thumbnail {imageSelector} description";

            return CreateCarouselSlide(
                parent,
                name,
                title,
                description,
                button1,
                button2,
                slideDesktopBackgroundImage,
                slideMobileBackgroundImage,
                slideThumbnailImage,
                thumbnailText);
        }
    }
}
