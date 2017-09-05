namespace TSC.Fixtures.Fixtures.Pages
{
    using System.Linq;

    using EPiServer.Core;

    using TSC.Fixtures.Entities;
    using TSC.Fixtures.Fixtures.Base;
    using TSC.Fixtures.Fixtures.Factory;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.Pages;
    using TSC.Website.Interfaces;
    using TSC.Website.Shared.Helpers;

    public class AccountListingFixtures : FixtureBase
    {
        private static string pageTitleExample = "Lorem ipsum dolor sit amet, consectetur";

        private static string pageSummary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras sodales dolor quis leo mattis ullamcorper. Donec vitae ligula at lorem vehicula iaculis.";

        private readonly BlockFixturesFactory blockFixturesFactory;

        private string mainContent =
            "<h2>Lorem ipsum dolor sit amet consectetur adipiscing elit.Cras sodales dolor quis leo mattis ullamcorper.</h2>";

        public AccountListingFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories, BlockFixturesFactory blockFixturesFactory)
            : base(websiteDependencies, episerverContentRepositories)
        {
            Guard.ValidateObject(blockFixturesFactory);
            this.blockFixturesFactory = blockFixturesFactory;
        }

        public AccountListingPage CreateAccountListingPage(
            StandardPageDetails standardPageDetails,
            ContentReference parentPage,
            ContentArea contentArea,
            string pageTitle,
            string pageSummary,
            string mainContent)
        {
            var existingPages =
                this.EpiserverContentRepositories.AccountListingPageRepository.GetChildren(parentPage);

            var existingPage = existingPages.FirstOrDefault(x => x.PageName == standardPageDetails.Name);
            if (existingPage != null)
            {
                return existingPage;
            }

            var accountListingPage = this.EpiserverContentRepositories.AccountListingPageRepository.CreateNewEmptyPage(parentPage);

            accountListingPage.Name = standardPageDetails.Name;
            accountListingPage.SeoTitle = standardPageDetails.SeoTitle;
            accountListingPage.Keywords = standardPageDetails.Keywords;
            accountListingPage.Description = new XhtmlString(standardPageDetails.Description);
            accountListingPage.PageTitle = pageTitle;
            accountListingPage.PageSummary = pageSummary;
            accountListingPage.MainContent = new XhtmlString(mainContent);
            accountListingPage.AccountArea = contentArea;

            var reference = this.EpiserverContentRepositories.AccountListingPageRepository.Save(accountListingPage);

            return reference != null
                ? accountListingPage
                : null;
        }

        public AccountListingPage CreateDummyAccountListingPage(ContentReference parent, string pageName)
        {
            var standardPageDetails = new StandardPageDetails();
            standardPageDetails.PopulateAll(pageName);
            var contentArea = CreateDummyContentArea(parent);

            return CreateAccountListingPage(
                standardPageDetails,
                parent,
                contentArea,
                pageTitleExample,
                pageSummary,
                mainContent);
        }

        private ContentArea CreateDummyContentArea(ContentReference parent)
        {
            var accountBlock1 = this.blockFixturesFactory.AccountBlockFixtures().CreateDummyAccountBlock1(parent);
            return new ContentArea
            {
                Items = { new ContentAreaItem { ContentLink = accountBlock1 } }
            };
        }
    }
}