namespace TSC.Fixtures.Fixtures.Blocks
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;
    using Resources;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using Website.Core.Blocks;
    using Website.Interfaces;

    public class TableBuilderFixtures : FixtureBase
    {
        public TableBuilderFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public ContentReference CreateTableBuilderBlock(ContentReference parent, string name, string tableStyle)
        {
            var existingTableBuilderBlock =
                this.WebsiteDependencies.ContentRepository.GetChildren<TableBuilderBlock>(parent)
                    .FirstOrDefault(block => block.ComponentName == name);

            if (existingTableBuilderBlock != null)
            {
                return (existingTableBuilderBlock as IContent).ContentLink;
            }

            var tableBuilderBlock =
                this.WebsiteDependencies.ContentRepository.GetDefault<TableBuilderBlock>(parent);
            tableBuilderBlock.TableHeadings = new XhtmlString(FixtureConstants.TableBuilderBlock.TableHeadings);
            tableBuilderBlock.HeaderClasses = FixtureConstants.TableBuilderBlock.HeaderClasses;
            tableBuilderBlock.TableContent = new XhtmlString(FixtureConstants.TableBuilderBlock.TableContent);
            tableBuilderBlock.TableStyle = tableStyle;
            tableBuilderBlock.ComponentName = name;

            var content = tableBuilderBlock as IContent;
            content.Name = name;

            return this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }
    }
}