namespace JonDJones.Website.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Entities;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.Pages.MetaPages.Menu;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class MenuPageRepository : IMenuPageRepository
    {
        private readonly IWebsiteDependencies _websiteDependencies;

        public MenuPageRepository(IWebsiteDependencies websiteDependencies)
        {
            Guard.ValidateObject(websiteDependencies);
            _websiteDependencies = websiteDependencies;
        }

        public List<INavigationItem> GetNavigationItems(PageReference pageReference)
        {
            if (PageReference.IsNullOrEmpty(pageReference))
            {
                return new List<INavigationItem>();
            }

            var navigationItems = new List<INavigationItem>();
            var menuPages =
                _websiteDependencies.ContentRepository.GetChildren<MenuPage>(pageReference);

            navigationItems.AddRange(menuPages.Select(CreateNavigationItem));
            return navigationItems;
        }

        public List<INavigationItem> GetMenuItems(StartPage startPage)
        {
            if (startPage == null || startPage.PrimaryNavigationContainerPage == null)
            {
                return new List<INavigationItem>();
            }

            var navigationItems = new List<INavigationItem>();

            var menuPages =
                _websiteDependencies.ContentRepository.GetChildren<MenuPage>(startPage.PrimaryNavigationContainerPage);

            foreach (var menuPage in menuPages)
            {
                var navigationItem = CreateNavigationItem(menuPage);

                var childMenuItems = _websiteDependencies.ContentRepository.GetChildren<MenuItemPage>(menuPage.ContentLink);
                navigationItem.SubMenuItems = childMenuItems.Select(CreateMenuItemPageItems);
                navigationItems.Add(navigationItem);
            }

            return navigationItems;
        }

        public IEnumerable<MenuPage> GetChildren(ContentReference parentPageReference)
        {
            return _websiteDependencies.ContentRepository.GetChildren<MenuPage>(parentPageReference);
        }

        public MenuPage CreateNewEmptyPage(ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);
            return _websiteDependencies.ContentRepository.GetDefault<MenuPage>(parentPageReference);
        }

        public MenuPage Get(int id)
        {
            return _websiteDependencies.ContentRepository.Get<MenuPage>(new ContentReference(id));
        }

        public MenuPage Get(string id)
        {
            return _websiteDependencies.ContentRepository.Get<MenuPage>(new ContentReference(id));
        }

        public bool Save(MenuPage stylesPage)
        {
            if (stylesPage == null)
            {
                return false;
            }

            var isPageModified = _websiteDependencies.ContextResolver.IsPageModified(stylesPage);
            if (!isPageModified)
            {
                return true;
            }

            _websiteDependencies.ContentRepository.Save(stylesPage, SaveAction.Publish, AccessLevel.NoAccess);
            return true;
        }

        public void Dispose()
        {
        }

        private NavigationItem CreateMenuItemPageItems(MenuItemPage menuItemPage)
        {
            return new NavigationItem
            {
                Name = menuItemPage.Name,
                Link = menuItemPage.LinkURL
            };
        }

        private NavigationItem CreateNavigationItem(MenuPage menuPage)
        {
            var url = _websiteDependencies.LinkResolver.GetFriendlyUrl(menuPage.MenuContentPage);
            var imageUrl = menuPage.MenuImageUrl == null
                ? string.Empty
                : _websiteDependencies.LinkResolver.GetFriendlyUrl(menuPage.MenuImageUrl);

            return new NavigationItem
                       {
                           Name = menuPage.MainMenuTitle ?? string.Empty,
                           SubMenuTitle = menuPage.SubMenuTitle ?? string.Empty,
                           ImageUrl = imageUrl,
                           Link = url
                       };
        }
    }
}