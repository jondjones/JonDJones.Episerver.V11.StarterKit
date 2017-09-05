namespace TSC.Fixtures.Fixtures.Blocks
{
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Fixtures.Fixtures.Factory;
    using TSC.Website.Core.Blocks;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Interfaces;
    using TSC.Website.Shared.Helpers;

    public class HubBlockFixtures : FixtureBase
    {
        private readonly HubItemBlockFixtures hubItemBlockFixtures;

        public HubBlockFixtures(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            HubItemBlockFixtures hubItemBlockFixtures)
            : base(websiteDependencies, episerverContentRepositories)
        {
            Guard.ValidateObject(hubItemBlockFixtures);
            this.hubItemBlockFixtures = hubItemBlockFixtures;
        }

        public ContentReference CreateHubBlock(
            ContentReference parent,
            string name,
            ContentArea contentArea)
        {
            var existingHubBlock =
                this.WebsiteDependencies.ContentRepository.GetChildren<HubBlock>(parent)
                    .FirstOrDefault();

            if (existingHubBlock != null)
            {
                return (existingHubBlock as IContent).ContentLink;
            }

            var hubBlock =
                this.WebsiteDependencies.ContentRepository.GetDefault<HubBlock>(parent);

            hubBlock.HubItems = contentArea;

            var content = hubBlock as IContent;
            content.Name = name;

            return this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference CreateDummyHubBlock(ContentReference parent)
        {
            var dummyHubItem = this.hubItemBlockFixtures.CreateDummyHubItemBlock(parent);

            var contentArea = new ContentArea();
            contentArea.Items.Add(new ContentAreaItem { ContentLink = dummyHubItem });
            contentArea.Items.Add(new ContentAreaItem { ContentLink = dummyHubItem });
            contentArea.Items.Add(new ContentAreaItem { ContentLink = dummyHubItem });

            return CreateHubBlock(parent, "Example Hub Block", contentArea);
        }
    }
}