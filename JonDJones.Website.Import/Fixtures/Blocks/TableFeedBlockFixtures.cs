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
    using TSC.Website.Shared.Resources;

    public class TableFeedBlockFixtures : FixtureBase
    {
        public TableFeedBlockFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public IContent CreateDataTableBlock(string name, string dataTableId, string dataModule, string dataEndPoint, string componentName)
        {
            var existingBlock = this.WebsiteDependencies.ContentRepository
                .GetChildren<TableFeedBlock>(ContentReference.GlobalBlockFolder)
                .FirstOrDefault(block => block.ComponentName == componentName);

            if (existingBlock != null)
            {
                return existingBlock as IContent;
            }

            var newBlock = this.WebsiteDependencies.ContentRepository.GetDefault<TableFeedBlock>(ContentReference.GlobalBlockFolder);
            newBlock.DataTableId = dataTableId;
            newBlock.ComponentName = componentName;
            newBlock.DataEndPoint = dataEndPoint;
            newBlock.DataModule = dataModule;

            var content = newBlock as IContent;
            if (content != null)
            {
                content.Name = name;

                this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
            }

            return content;
        }

        public IContent CreateRecommendedSharesBLock()
        {
            return this.CreateDataTableBlock(
                FixtureConstants.PageNames.RecommendedSharesPageName,
                GlobalConstants.TableFeedConfig.RecommendedDataTableId,
                GlobalConstants.TableFeedConfig.RecommendedDataModule,
                GlobalConstants.TableFeedConfig.RecommendedEndPoint,
                GlobalConstants.TableFeedConfig.RecommendedComponentName);
        }

        public IContent CreateRisersBlock()
        {
            return this.CreateDataTableBlock(
                FixtureConstants.BlockNames.RisersBlockName,
                GlobalConstants.TableFeedConfig.RisersDataTableId,
                GlobalConstants.TableFeedConfig.RisersDataModule,
                GlobalConstants.TableFeedConfig.RisersEndPoint,
                GlobalConstants.TableFeedConfig.RisersComponentName);
        }

        public IContent CreateFallersBlock()
        {
            return this.CreateDataTableBlock(
                FixtureConstants.BlockNames.FallersBlockName,
                GlobalConstants.TableFeedConfig.FallersDataTableId,
                GlobalConstants.TableFeedConfig.FallersDataModule,
                GlobalConstants.TableFeedConfig.FallersEndPoint,
                GlobalConstants.TableFeedConfig.FallersComponentName);
        }
    }
}
