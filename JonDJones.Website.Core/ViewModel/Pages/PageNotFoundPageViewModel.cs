namespace JonDJones.Website.Core.ViewModel.Pages
{
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;

    public class PageNotFoundPageViewModel : BaseViewModel<PageNotFoundPage>
    {
        public PageNotFoundPageViewModel(PageNotFoundPage currentPage, IWebsiteDependencies websiteWebsiteDependencies)
            : base(currentPage, websiteWebsiteDependencies)
        {
        }
    }
}