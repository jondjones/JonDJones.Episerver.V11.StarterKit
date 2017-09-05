namespace JonDJones.Website.Core.ViewModel.Base
{
    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class BaseViewModel<T> : IPageViewModel<T> where T : GlobalBasePage
    {
        public BaseViewModel(T currentPage, IWebsiteDependencies websiteWebsiteDependencies)
        {
            Guard.ValidateObject(websiteWebsiteDependencies);
            Guard.ValidateObject(currentPage);

            CurrentPage = currentPage;
            WebsiteDependencies = websiteWebsiteDependencies;
        }

        public T CurrentPage { get; }

        public ILayoutViewModel Layout { get; set; }

        public IWebsiteDependencies WebsiteDependencies { get; }
    }
}