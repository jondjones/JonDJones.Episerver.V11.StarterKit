namespace TSC.Fixtures.Fixtures.Blocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Blocks;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.Entities;
    using TSC.Website.Interfaces;

    public class AccountBlockFixtures : FixtureBase
    {
        private string openAccountTextExample = "Open Account";

        private string findOutMoreTextExample = "Find out more";

        private string expandAccountTextExample = "ExpandAccountText";

        private string homepageUrl = "/";

        private string costsExplainedExample = "£1.50 +VAT per month admin fee <br/>Dealing commission from £7.50 per trade";

        private string accountTitleExample = "Share account";

        private string accountOverviewExample =
            "Your day-to-day investment account. Buy and sell a wide range of investments with no investment limits.";

        private List<OrderedListItem> accountHighlights =
            new List<OrderedListItem> { new OrderedListItem { Description = "Tick lorem ipsum" }, new OrderedListItem { Description = "Tick lorem ipsum" } };

        public AccountBlockFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories)
            : base(websiteDependencies, episerverContentRepositories)
        {
        }

        public ContentReference CreateAccountBlock(
            string name,
            ContentReference parent,
            string accountTitle,
            string accountOverview,
            IList<OrderedListItem> accountBenefits,
            string costsExplained,
            string accountOpeningButtonText,
            string accountOpeningButtonUrl,
            string findOutMoreText,
            string findOutMoreUrl,
            string expandAccountText,
            bool openByDefaultOnMobile = true)
        {
            var existingRichTextBlock =
                this.WebsiteDependencies.ContentRepository.GetChildren<AccountBlock>(parent)
                    .FirstOrDefault();

            if (existingRichTextBlock != null)
            {
                return (existingRichTextBlock as IContent).ContentLink;
            }

            var accountBlock =
                this.WebsiteDependencies.ContentRepository.GetDefault<AccountBlock>(parent);

            accountBlock.AccountTitle = accountTitle;
            accountBlock.AccountOverview = new XhtmlString(accountOverview);
            accountBlock.AccountBenefits = accountBenefits;
            accountBlock.CostsExplained = new XhtmlString(costsExplained);
            accountBlock.AccountOpeningButtonText = accountOpeningButtonText;
            accountBlock.AccountOpeningButtonUrl = new Url(accountOpeningButtonUrl);
            accountBlock.FindOutMoreText = findOutMoreText;
            accountBlock.FindOutMoreUrl = new Url(findOutMoreUrl);
            accountBlock.OpenByDefaultOnMobile = openByDefaultOnMobile;
            accountBlock.ExpandAccountText = expandAccountText;

            var content = accountBlock as IContent;
            content.Name = name;

            return this.WebsiteDependencies.ContentRepository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference CreateDummyAccountBlock1(ContentReference parent)
        {
            return CreateAccountBlock(
                "Account 1",
                parent,
                this.accountTitleExample,
                this.accountOverviewExample,
                this.accountHighlights,
                this.costsExplainedExample,
                this.openAccountTextExample,
                this.homepageUrl,
                this.findOutMoreTextExample,
                this.homepageUrl,
                this.expandAccountTextExample);
        }
    }
}
