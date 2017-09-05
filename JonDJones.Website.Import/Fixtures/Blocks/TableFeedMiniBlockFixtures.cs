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

    public class TableFeedMiniBlockFixtures : FixtureBase
    {
        public TableFeedMiniBlockFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public IContent CreateTableFeedMiniBlock(
            string name,
            string dataTableId,
            string dataModule,
            string dataEndPoint,
            string componentName,
            string title,
            string subTitle,
            PageReference seeAllLink,
            bool includeFooterText)
        {
            var existingBlock = this.WebsiteDependencies.ContentRepository
                .GetChildren<TableFeedMiniBlock>(ContentReference.GlobalBlockFolder)
                .FirstOrDefault(block => block.ComponentName == componentName);

            if (existingBlock != null)
            {
                return existingBlock as IContent;
            }

            var newBlock = this.WebsiteDependencies.ContentRepository.GetDefault<TableFeedMiniBlock>(ContentReference.GlobalBlockFolder);
            newBlock.DataTableId = dataTableId;
            newBlock.ComponentName = componentName;
            newBlock.DataEndPoint = dataEndPoint;
            newBlock.DataModule = dataModule;
            newBlock.Title = title;
            newBlock.SubTitle = subTitle;
            newBlock.SeeAllLink = seeAllLink;
            newBlock.IncludeFooterText = includeFooterText;

            var content = newBlock as IContent;
            if (content != null)
            {
                content.Name = name;

                this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
            }

            return content;
        }

        public IContent CreateRisersMiniBlock(PageReference seeAllPageReference)
        {
            return this.CreateTableFeedMiniBlock(
                FixtureConstants.BlockNames.RisersMiniBlockName,
                GlobalConstants.TableFeedMiniConfig.RisersDataTableId,
                GlobalConstants.TableFeedMiniConfig.RisersDataModule,
                GlobalConstants.TableFeedMiniConfig.RisersEndPoint,
                GlobalConstants.TableFeedMiniConfig.RisersComponentName,
                GlobalConstants.TableFeedMiniConfig.RisersTitle,
                GlobalConstants.TableFeedMiniConfig.RisersSubTitle,
                seeAllPageReference,
                true);
        }

        public IContent CreateFallersMiniBlock(PageReference seeAllPageReference)
        {
            return this.CreateTableFeedMiniBlock(
                FixtureConstants.BlockNames.FallersMiniBlockName,
                GlobalConstants.TableFeedMiniConfig.FallersDataTableId,
                GlobalConstants.TableFeedMiniConfig.FallersDataModule,
                GlobalConstants.TableFeedMiniConfig.FallersEndPoint,
                GlobalConstants.TableFeedMiniConfig.FallersComponentName,
                GlobalConstants.TableFeedMiniConfig.FallersTitle,
                GlobalConstants.TableFeedMiniConfig.FallersSubTitle,
                seeAllPageReference,
                true);
        }
    }
}
