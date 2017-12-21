namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces
{
    public interface IPageTypeServices
    {
        IMenuService MenuService { get; }

        IStartPageService StartPageService { get; }

        IKeyValueService KeyValueService { get; }

        ISiteSettingsService SiteSettingsService { get; }

        IContentPageService ContentPageService { get; }
    }
}
