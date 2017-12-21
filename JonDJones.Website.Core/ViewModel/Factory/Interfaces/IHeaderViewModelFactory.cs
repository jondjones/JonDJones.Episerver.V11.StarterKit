using JonDJones.Website.Core.Pages;
using JonDJones.Website.Interfaces;

namespace JonDJones.Website.Core.ViewModel.Factory.Interfaces
{
    public interface IHeaderViewModelFactory
    {
        IHeaderViewModel CreateHeaderProperties(
            StartPage startPage,
            ISiteSettingsProperties siteSettingsProperties);
    }
}
