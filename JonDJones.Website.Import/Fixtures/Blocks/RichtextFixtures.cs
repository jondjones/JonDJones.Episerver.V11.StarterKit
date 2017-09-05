namespace JonDJones.Fixtures.Fixtures.Blocks
{
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using JonDJones.Fixtures.Fixtures.Base;
    using JonDJones.Fixtures.Helpers;
    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;

    public class RichtextFixtures : FixtureBase
    {
        public RichtextFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public ContentReference CreateRichTextBlock(ContentReference parent, string name, string text)
        {
            var existingRichTextBlock =
                WebsiteDependencies.ContentRepository.GetChildren<RichTextBlock>(parent)
                    .FirstOrDefault();

            if (existingRichTextBlock != null)
            {
                return (existingRichTextBlock as IContent).ContentLink;
            }

            var richTextBlock =
                WebsiteDependencies.ContentRepository.GetDefault<RichTextBlock>(parent);
            richTextBlock.Text = new XhtmlString(text);

            var content = richTextBlock as IContent;
            content.Name = name;

            return WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference CreateDummyRichTextBlock()
        {
            return CreateRichTextBlock(ContentReference.GlobalBlockFolder, "Dummy Rich Text Block", ContentHelper.LoremIpsum);
        }
    }
}
