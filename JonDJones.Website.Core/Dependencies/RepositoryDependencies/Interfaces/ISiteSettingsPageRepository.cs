﻿namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces
{
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Interfaces;

    public interface ISiteSettingsPageRepository : IRepository<SiteSettingsPage>
    {
        SiteSettingsPage SiteSettingsPage { get; }
    }
}