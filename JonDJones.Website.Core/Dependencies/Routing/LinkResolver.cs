namespace JonDJones.Website.Core.Dependencies.Routing
{
    using System;
    using System.Web.Mvc;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.ServiceLocation;
    using EPiServer.SpecializedProperties;
    using EPiServer.Web.Mvc.Html;
    using EPiServer.Web.Routing;
    using log4net;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class LinkResolver : ILinkResolver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LinkResolver));

        private readonly UrlResolver _urlResolver;

        private readonly IContentRepository _contentRepository;

        public LinkResolver(IContentRepository contentRepository, UrlResolver urlResolver)
        {
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(urlResolver);
            _contentRepository = contentRepository;
            _urlResolver = urlResolver;
        }

         public string GetFriendlyUrl(Url internalUrl)
         {
            var urlHelper = ServiceLocator.Current.GetInstance<UrlHelper>();
            var url = string.Empty;

            if (string.IsNullOrEmpty(internalUrl?.ToString()))
            {
                return url;
            }

            var friendlyUrl = urlHelper.ContentUrl(internalUrl.ToString());
            return friendlyUrl;
        }

        public string GetFriendlyUrl(ContentReference pageLink)
        {
            var virtualPath = this._urlResolver.GetVirtualPath(pageLink);
            return virtualPath?.VirtualPath;
        }

        public string ResolveUrl(string originalString)
        {
            return originalString;
        }

        public string ResolveUrl(IContent content)
        {
            return content == null ? string.Empty : ResolveContentReferenceUrl(content.ContentLink);
        }

        public string ResolveUrl(Url url)
        {
            if (url == null)
            {
                return string.Empty;
            }

            var parsedUrl = ResolveUrl(PageReference.ParseUrl(url.OriginalString));
            return string.IsNullOrWhiteSpace(parsedUrl) ? url.OriginalString : parsedUrl;
        }

        public string ResolveUrl(ContentReference contentReference)
        {
            if (ContentReference.IsNullOrEmpty(contentReference))
            {
                return string.Empty;
            }

            var content = this._contentRepository.Get<IContent>(contentReference);
            return ResolveUrl(content);
        }

        public string ResolveUrl(LinkItem linkItem)
        {
            var result = linkItem.Href;
            var url = new UrlBuilder(linkItem.ToMappedLink());

            int id;
            if (int.TryParse(url.QueryCollection["id"], out id))
            {
                result = ResolveUrl(new ContentReference(id));
            }

            return result;
        }

        private string ResolveContentReferenceUrl(ContentReference contentReference)
        {
            if (ContentReference.IsNullOrEmpty(contentReference))
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = this._urlResolver.GetUrl(contentReference) ?? string.Empty;
            }
            catch (Exception e)
            {
                Logger.ErrorFormat("Error attempting to resolve URL.\r\n{0}", e);
            }

            return result;
        }
    }
}