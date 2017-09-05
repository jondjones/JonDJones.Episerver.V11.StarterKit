namespace JonDJones.Website.Interfaces
{
    using EPiServer;
    using EPiServer.DataAbstraction;
    using EPiServer.Web.Routing;

    public interface IWebsiteDependencies
    {
        ILinkResolver LinkResolver { get; }

        IAssetHandler AssetHandler { get; }

        IContentRepository ContentRepository { get; }

        IContentTypeRepository ContentTypeRepository { get; }

        IContextResolver ContextResolver { get; }

        IContentRoute ContentRoute { get; }

        ICacheManager CacheManager { get; }

        ISiteSettingsProperties SiteSettings { get; }
    }
}