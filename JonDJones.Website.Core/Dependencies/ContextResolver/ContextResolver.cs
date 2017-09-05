namespace JonDJones.Website.Core.Dependencies.ContextResolver
{
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Editor;
    using EPiServer.Web;
    using EPiServer.Web.PageExtensions;
    using EPiServer.Web.Routing;

    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class ContextResolver : IContextResolver
    {
        private readonly IContentRepository _repository;

        private readonly IPageRouteHelper _pageRouteHelper;

        public ContextResolver(IContentRepository repository, IPageRouteHelper pageRouteHelper)
        {
            Guard.ValidateObject(repository);
            Guard.ValidateObject(pageRouteHelper);
            _repository = repository;
            _pageRouteHelper = pageRouteHelper;
        }

        public PageData CurrentPage => this._pageRouteHelper.Page;

        public bool IsInEditMode => PageEditing.PageIsInEditMode;

        public PageReference RootPage => ContentReference.RootPage;

        public PageReference StartPage => ContentReference.StartPage;

        public ContentReference GlobalBlockFolder => ContentReference.GlobalBlockFolder;

        public ContentFolder SiteAssetsFolder => this._repository.GetDefault<ContentFolder>(SiteDefinition.Current.SiteAssetsRoot);

        public bool IsPageModified(PageData page)
        {
            return page.IsModified;
        }

        public int GetIdFromBlock(BlockData block)
        {
            Guard.ValidateObject(block);
            return ((IContent)block).ContentLink.ID;
        }

        public ContentArea AddContentAreaItem(ContentArea contentArea, ContentAreaItem contentAreaItem)
        {
            contentArea.Items.Add(contentAreaItem);
            return contentArea;
        }

        public bool HasContentAreaGotItems(ContentArea contentArea)
        {
            return contentArea != null && contentArea.Items.Count > 1;
        }
    }
}