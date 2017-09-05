namespace TSC.Fixtures.Fixtures.Pages
{
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Fixtures.Helpers;
    using TSC.Website.Core.Pages;
    using TSC.Website.Interfaces;

    public class RecommendedSharesPageFixtures : FixtureBase
    {
        public RecommendedSharesPageFixtures(IWebsiteDependencies dependencies)
            : base(dependencies)
        {
        }

        public RecommendedSharesPage CreateNewsPage(string pageName, ContentHelper contentHelper)
        {
            var newsItemPages =
                Dependencies.ContentRepository.GetChildren<RecommendedSharesPage>(Dependencies.ContextResolver.StartPage)
                    .ToList();

            if (newsItemPages.Any(x => x.Name == pageName))
            {
                return newsItemPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = Dependencies.ContentRepository.GetDefault<NewsItemPage>(
                Dependencies.ContextResolver.StartPage);

            newPage.Name = pageName;
            newPage.PageTitle = pageName;
            newPage.SeoTitle = pageName;
            newPage.Keywords = pageName;
            newPage.Description = new XhtmlString(pageName);
            newPage.MainContentArea = contentHelper.CreateDummyContentArea();

            var reference = Dependencies.ContentRepository.Save(newPage, SaveAction.Publish, AccessLevel.NoAccess);

            return reference != null ? newPage : null;
        }
    }
}