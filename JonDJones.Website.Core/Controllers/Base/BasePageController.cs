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

        private Injected<IPageTypeServices> pageServices;

        public IWebsiteDependencies WebsiteDependencies => websiteDependenciesService.Service;

        public IPageTypeServices PageServices => pageServices.Service;
    }
}