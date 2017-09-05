namespace JonDJones.Website.Core.ViewModel.Pages
{
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;

    public class ContentPageViewModel : BaseViewModel<ContentPage>
    {
        public ContentPageViewModel(ContentPage currentPage, IWebsiteDependencies websiteWebsiteDependencies)
            : base(currentPage, websiteWebsiteDependencies)
        {
        }
    }
}