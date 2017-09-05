namespace JonDJones.Website.Core.Controllers.Base
{
    using EPiServer.Core;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Mvc;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;

    public class BasePageController<T> : PageController<T>
        where T : PageData
    {
        private Injected<IWebsiteDependencies> websiteDependenciesService;

        private Injected<IEpiserverContentRepositories> episerverRepositoryDependencies;

        public IWebsiteDependencies WebsiteDependencies => websiteDependenciesService.Service;

        public IEpiserverContentRepositories EpiserverRepositoryDependencies => episerverRepositoryDependencies.Service;
    }
}