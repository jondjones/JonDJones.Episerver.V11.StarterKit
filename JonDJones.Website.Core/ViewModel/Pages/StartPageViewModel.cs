namespace JonDJones.Website.Core.ViewModel.Pages
{
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;

    public class StartPageViewModel : BaseViewModel<StartPage>
    {
        public StartPageViewModel(StartPage currentPage, IWebsiteDependencies websiteWebsiteDependencies)
            : base(currentPage, websiteWebsiteDependencies)
        {
        }
    }
}