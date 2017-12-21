using JonDJones.Website.Core.Pages;
using JonDJones.Website.Interfaces;

namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces
{
    public interface ISiteSettingsService : IRepository<SiteSettingsPage>
    {
        SiteSettingsPage SiteSettingsPage { get; }
    }
}