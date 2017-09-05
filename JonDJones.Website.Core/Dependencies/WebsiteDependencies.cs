namespace JonDJones.Website.Core.Dependencies
{
    using EPiServer;
    using EPiServer.DataAbstraction;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Routing;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class WebsiteDependencies : IWebsiteDependencies
    {
        private IContentTypeRepository _contentTypeRepository;

        private ILinkResolver _linkResolver;

        private IContentRoute _contentRoute;

        private ISiteSettingsPageRepository _siteSettingsPageRepository;

        public WebsiteDependencies(
            IContentRepository contentRepository,
            IContextResolver contextResolver,
            ICacheManager cacheManager,
            IAssetHandler assetHandler,
            ISiteSettingsPageRepository siteSettingsPageRepository)
        {
            Guard.ValidateObject(cacheManager);
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(contextResolver);
            Guard.ValidateObject(assetHandler);
            Guard.ValidateObject(siteSettingsPageRepository);

            CacheManager = cacheManager;
            ContentRepository = contentRepository;
            ContextResolver = contextResolver;
            AssetHandler = assetHandler;
            _siteSettingsPageRepository = siteSettingsPageRepository;
        }

        public ILinkResolver LinkResolver => _linkResolver
            ?? (_linkResolver = ServiceLocator.Current.GetInstance<ILinkResolver>());

        public IContentRepository ContentRepository { get; }

        public IContentTypeRepository ContentTypeRepository => _contentTypeRepository
            ?? (_contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>());

        public IContextResolver ContextResolver { get; }

        public IContentRoute ContentRoute => _contentRoute
            ?? (_contentRoute = ServiceLocator.Current.GetInstance<IContentRoute>());

        public IAssetHandler AssetHandler { get; }

        public ICacheManager CacheManager { get; }

        public ISiteSettingsProperties SiteSettings => _siteSettingsPageRepository.SiteSettingsPage;
    }
}