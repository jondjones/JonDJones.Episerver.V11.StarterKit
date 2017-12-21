using EPiServer.Framework;
using System;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;

namespace JonDJones.Fixtures
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class FixtureStartupModule : IInitializableModule
    {
        public Injected<IWebsiteDependencies> WebsiteDependencies { get; set; }

        public Injected<IPageTypeServices> PageTypeServices { get; set; }

        public void Initialize(InitializationEngine context)
        {
            var fixtureSetup = new FixturesSetup(WebsiteDependencies.Service, PageTypeServices.Service);
            fixtureSetup.SetupWebsite();
        }

        public void Uninitialize(InitializationEngine context)
        {
            throw new NotImplementedException();
        }
    }
}
