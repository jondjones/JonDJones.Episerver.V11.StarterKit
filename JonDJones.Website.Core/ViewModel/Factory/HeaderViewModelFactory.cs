using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.Core.Entities;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Core.ViewModel.Factory.Interfaces;
using JonDJones.Website.Core.ViewModel.Shared;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Website.Core.ViewModel.Factory
{
    public class HeaderViewModelFactory : IHeaderViewModelFactory
    {
        private readonly IMenuService _menuPageRepository;

        public HeaderViewModelFactory( IMenuService menuPageRepository)
        {
            Guard.ValidateObject(menuPageRepository);
            _menuPageRepository = menuPageRepository;
        }

        public IHeaderViewModel CreateHeaderProperties(
            StartPage startPage,
            ISiteSettingsProperties siteSettingsProperties)
        {
            Guard.ValidateObject(startPage);
            Guard.ValidateObject(siteSettingsProperties);

            var menuProperties = new HeaderMenuProperties
            {
                MenuItems = _menuPageRepository.GetMenuItems(startPage),
            };

            var headerViewModel = new HeaderViewModel(startPage)
            {
                SiteSettingsProperties = siteSettingsProperties,
                MenuProperties = menuProperties
            };

            return headerViewModel;
        }
    }
}
