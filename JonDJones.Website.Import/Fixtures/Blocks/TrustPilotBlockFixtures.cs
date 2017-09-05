namespace TSC.Fixtures.Fixtures.Blocks
{
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Blocks;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Interfaces;
    using TSC.Website.Interfaces.Enums;

    public class TrustPilotBlockFixtures : FixtureBase
    {
        public TrustPilotBlockFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public ContentReference CreateTrustPilotBlock(ContentReference parent, string name, string style)
        {
            var existingTrustPilotBlock = this.WebsiteDependencies.ContentRepository
                .GetChildren<TrustPilotBlock>(parent)
                .FirstOrDefault();

            if (existingTrustPilotBlock != null)
            {
                return (existingTrustPilotBlock as IContent).ContentLink;
            }

            var trustPilotBlockBlock = this.WebsiteDependencies.ContentRepository.GetDefault<TrustPilotBlock>(parent);
            trustPilotBlockBlock.Style = style;
            var content = trustPilotBlockBlock as IContent;
            content.Name = name;

            return this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }
    }
}
