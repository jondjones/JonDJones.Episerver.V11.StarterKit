using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies
{
    public class PageTypeServices : IPageTypeServices
    {
        public PageTypeServices(
            IMenuService menuPageRepository,
            IStartPageService startPageRepository,
            ISiteSettingsService siteSettingsPageRepository,
            IContentPageService contentPageService,
            IKeyValueService keyValueRepository)
        {
            Guard.ValidateObject(menuPageRepository);
            Guard.ValidateObject(startPageRepository);
            Guard.ValidateObject(keyValueRepository);
            Guard.ValidateObject(siteSettingsPageRepository);
            Guard.ValidateObject(contentPageService);

            MenuService = menuPageRepository;
            StartPageService = startPageRepository;
            KeyValueService = keyValueRepository;
            SiteSettingsService = siteSettingsPageRepository;
            ContentPageService = contentPageService;
        }

        public IMenuService MenuService { get; }

        public IStartPageService StartPageService { get; }

        public IContentPageService ContentPageService { get; }

        public IKeyValueService KeyValueService { get; }

        public ISiteSettingsService SiteSettingsService { get; }
    }
}