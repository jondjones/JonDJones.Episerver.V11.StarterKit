namespace TSC.Fixtures.Fixtures.Pages
{
    using EPiServer.Core;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.Pages;
    using TSC.Website.Interfaces;
    using TSC.Website.Shared.Helpers;

    public class ResourcePageFixtures : FixturePageBase
    {
        public ResourcePageFixtures(IWebsiteDependencies websiteDependencies, IEpiserverContentRepositories episerverContentRepositories, IContent homepage)
            : base(websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        public SiteResourcePage CreateSiteResourcePage(
            string pageName,
            ContentReference parentPageReference,
            string awardsLabel,
            string openAccountLabelDefault,
            string screenReaderSearchLabel,
            string searchLabel,
            string searchPlaceholderLabel,
            string socialMediaLabel,
            string myAccountLabel,
            string signInLabel,
            string signOutLabel,
            string usefulLinksText,
            string hideHubText)
        {
            Guard.ValidateObject(parentPageReference);

            var existingSiteSettingsPage = this.EpiserverContentRepositories.SiteResourcePageRepository.SiteResourcePage;

            if (existingSiteSettingsPage != null)
            {
                return existingSiteSettingsPage;
            }

            var siteResourcePage = this.EpiserverContentRepositories.SiteResourcePageRepository.CreateNewEmptyPage(parentPageReference);

            siteResourcePage.Name = pageName;
            siteResourcePage.AwardsLabel = awardsLabel;
            siteResourcePage.OpenAccountLabel = openAccountLabelDefault;
            siteResourcePage.ScreenReaderSearchLabel = screenReaderSearchLabel;
            siteResourcePage.SearchLabel = searchLabel;
            siteResourcePage.SearchPlaceholderLabel = searchPlaceholderLabel;
            siteResourcePage.SocialMediaLabel = socialMediaLabel;
            siteResourcePage.MyAccountLabel = myAccountLabel;
            siteResourcePage.SignInLabel = signInLabel;
            siteResourcePage.SignOutLabel = signOutLabel;
            siteResourcePage.UsefulLinksText = usefulLinksText;
            siteResourcePage.HideHubText = hideHubText;
            siteResourcePage.TableFeedMiniStandardFooterText = Properties.Resources.TableFeedMiniFooterText;
            siteResourcePage.TableFeedMiniTableDetailsTitle = Properties.Resources.TableFeedMiniTableDetails;
            siteResourcePage.TableFeedStandardFooterText = Properties.Resources.TableFeedFooterText;
            siteResourcePage.TableFeedTableDetailsTitle = Properties.Resources.TableFeedTableDetails;
            siteResourcePage.TableFeedFilterButtonText = Properties.Resources.TableFeedFilterButtonText;

            return this.EpiserverContentRepositories.SiteResourcePageRepository.Save(siteResourcePage)
                       ? siteResourcePage
                       : null;
        }

        public SiteResourcePage CreateDummySiteResourcePage(
            string pageName,
            ContentReference parentPageReference)
        {
            return CreateSiteResourcePage(
                pageName,
                parentPageReference,
                Properties.Resources.AwardsLabelDefault,
                Properties.Resources.OpenAccountLabelDefault,
                Properties.Resources.ScreenReaderSearchLabelDefault,
                Properties.Resources.SearchLabelDefault,
                Properties.Resources.SearchPlaceholderLabelDefault,
                Properties.Resources.SocialMediaLabelDefault,
                Properties.Resources.MyAccountLabelDefault,
                Properties.Resources.SignInLabelDefault,
                Properties.Resources.SignOutLabelDefault,
                Properties.Resources.UsefulLinksText,
                Properties.Resources.HideHubText);
        }
    }
}