namespace TSC.Fixtures.Fixtures.Blocks
{
    using System.Linq;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Blocks;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Interfaces;

    public class HubItemBlockFixtures : FixtureBase
    {
        public HubItemBlockFixtures(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public ContentReference CreateHubItemBlock(
            ContentReference parent,
            string name,
            string title,
            string hubTitle,
            string hubDescription,
            string desktopBackgroundImage,
            string mobileBackgroundImage)
        {
            var existingHubBlock =
                this.WebsiteDependencies.ContentRepository.GetChildren<HubItemBlock>(parent)
                    .FirstOrDefault();

            if (existingHubBlock != null)
            {
                return (existingHubBlock as IContent).ContentLink;
            }

            var hubBlock =
                this.WebsiteDependencies.ContentRepository.GetDefault<HubItemBlock>(parent);

            hubBlock.Title = title;
            hubBlock.HubTitle = hubTitle;
            hubBlock.HubDescription = new XhtmlString(hubDescription);
            hubBlock.DesktopBackgroundImage = new Url(desktopBackgroundImage);
            hubBlock.MobileBackgroundImage = new Url(mobileBackgroundImage);

            var content = hubBlock as IContent;
            content.Name = name;

            return this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference CreateDummyHubItemBlock(ContentReference parent)
        {
            var title = "Lorem ipsum dolor mit amet mit consectetur amer";
            var hubTitle = "Everything you need to know about ISAs";
            var hubDescription =
                "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore. magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip.";

            var desktopBackgroundImage = "http://lorempixel.com/363/306/abstract/6/";
            var mobileBackgroundImage = "http://lorempixel.com/320/160/abstract/7/";

            return CreateHubItemBlock(
                parent,
                "Hub Example",
                title,
                hubTitle,
                hubDescription,
                desktopBackgroundImage,
                mobileBackgroundImage);
        }
    }
}
