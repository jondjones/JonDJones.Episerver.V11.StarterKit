namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies
{
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class EpiserverContentRepositories : IEpiserverContentRepositories
    {
        public EpiserverContentRepositories(
            IMenuPageRepository menuPageRepository,
            IStartPageRepository startPageRepository,
            ISiteSettingsPageRepository siteSettingsPageRepository,
            IKeyValueRepository keyValueRepository)
        {
            Guard.ValidateObject(menuPageRepository);
            Guard.ValidateObject(startPageRepository);
            Guard.ValidateObject(keyValueRepository);
            Guard.ValidateObject(siteSettingsPageRepository);

            MenuPageRepository = menuPageRepository;
            StartPageRepository = startPageRepository;
            KeyValueRepository = keyValueRepository;
            SiteSettingsPageRepository = siteSettingsPageRepository;
        }

        public IMenuPageRepository MenuPageRepository { get; }

        public IStartPageRepository StartPageRepository { get; }

        public IKeyValueRepository KeyValueRepository { get; }

        public ISiteSettingsPageRepository SiteSettingsPageRepository { get; }
    }
}