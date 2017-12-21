using System.Collections.Generic;

using EPiServer.Core;

using JonDJones.Website.Core.Pages;
using JonDJones.Website.Core.Pages.MetaPages.Menu;
using JonDJones.Website.Interfaces;

namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces
{
    public interface IMenuService : IRepository<MenuPage>
    {
        List<INavigationItem> GetNavigationItems(PageReference pageReference);

        List<INavigationItem> GetMenuItems(StartPage startPage);
    }
}