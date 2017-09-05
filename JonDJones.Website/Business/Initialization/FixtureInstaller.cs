﻿namespace JonDJones.Website.Business.Initialization
{
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;
    using Fixtures;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Resources;
    using JonDJones.Website.Interfaces;

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class FixtureInitialization : IInitializableModule
    {
        internal Injected<IWebsiteDependencies> WebsiteDependencies { get; set; }

        internal Injected<IEpiserverContentRepositories> EpiserverContentRepositories { get; set; }

        public void Initialize(InitializationEngine context)
        {
            if (!AppSettings.RunFixtures)
            {
                return;
            }

            var fixturesSetup = new FixturesInstaller(WebsiteDependencies.Service, EpiserverContentRepositories.Service);
            fixturesSetup.SetupWebsite();
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}