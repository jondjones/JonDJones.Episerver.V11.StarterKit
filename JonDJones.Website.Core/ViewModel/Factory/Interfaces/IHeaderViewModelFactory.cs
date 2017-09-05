namespace JonDJones.Website.Core.ViewModel.Factory.Interfaces
{
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Interfaces;

    public interface IHeaderViewModelFactory
    {
        IHeaderViewModel CreateHeaderProperties(
            StartPage startPage,
            ISiteSettingsProperties siteSettingsProperties);
    }
}
