namespace JonDJones.Website.Core.ViewModel.Blocks
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;

    using Dependencies.RepositoryDependencies.Interfaces;

    using EPiServer;

    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.Extensions;
    using JonDJones.Website.Core.ViewModel.Base;

    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Interfaces.Enums;
    using Website.Shared.Helpers;

    public class IFrameBlockViewModel : BlockViewModel<IFrameBlock>
    {
        public IFrameBlockViewModel(
            IFrameBlock currentBlock,
            IWebsiteDependencies _websiteDependencies,
            DisplayOptionEnum displayOptionTag)
            : base(currentBlock, _websiteDependencies, displayOptionTag)
        {
        }

        public string LinkUrl => QueryStringNameValueCollection.HasKeys()
                                     ? BuildUri(
                                         WebsiteDependencies.LinkResolver.GetFriendlyUrl(CurrentBlock.Hyperlink),
                                         QueryStringNameValueCollection).AbsoluteUri
                                     : BuildUri(
                                         WebsiteDependencies.LinkResolver.GetFriendlyUrl(CurrentBlock.Hyperlink),
                                         QueryStringNameValueCollection).ToString();

        public NameValueCollection QueryStringNameValueCollection { get; set; }

        private Uri BuildUri(Url root, NameValueCollection query)
        {
            var urlRoot = WebsiteDependencies.LinkResolver.GetFriendlyUrl(root);
            var collection = HttpUtility.ParseQueryString(string.Empty);

            foreach (var key in query.Cast<string>().Where(key => !string.IsNullOrEmpty(query[key])))
            {
                collection[key] = query[key];
            }

            var builder = new UriBuilder(urlRoot) { Query = collection.ToString() };
            return builder.Uri;
        }
    }
}