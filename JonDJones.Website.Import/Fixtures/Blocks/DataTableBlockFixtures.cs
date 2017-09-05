namespace TSC.Fixtures.Fixtures.Blocks
{
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Fixtures.Resources;
    using TSC.Website.Core.Blocks;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Interfaces;

    public class DataTableBlockFixtures : FixtureBase
    {
        public DataTableBlockFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public DataTableBlock CreateDataTableBlock(ContentReference parent, string name, string dataModule, string endPoint, string componentName)
        {
            var existingBlock = this.WebsiteDependencies.ContentRepository
                .GetChildren<DataTableBlock>(parent)
                .FirstOrDefault(block => block.ComponentName == componentName);

            if (existingBlock != null)
            {
                return existingBlock;
            }

            var newBlock = this.WebsiteDependencies.ContentRepository.GetDefault<DataTableBlock>(parent);
            newBlock.ComponentName = componentName;
            newBlock.EndPoint = endPoint;
            newBlock.DataModule = dataModule;

            var content = newBlock as IContent;
            content.Name = name;

            this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
            return newBlock;
        }

        public DataTableBlock CreateExampleRecommendedSharesBlock()
        {
            return CreateDataTableBlock(
                ContentReference.GlobalBlockFolder,
                FixtureConstants.PageNames.RecommendedSharesPageName,
                FixtureConstants.RecommendedSharesConfig.DefaultDataModule,
                FixtureConstants.RecommendedSharesConfig.DefaultEndPoint,
                FixtureConstants.RecommendedSharesConfig.DefaultComponentName);
        }
    }
}
