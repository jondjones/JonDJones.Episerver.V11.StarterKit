namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces
{
    public interface IEpiserverContentRepositories
    {
        IMenuPageRepository MenuPageRepository { get; }

        IStartPageRepository StartPageRepository { get; }

        IKeyValueRepository KeyValueRepository { get; }

        ISiteSettingsPageRepository SiteSettingsPageRepository { get; }
    }
}
