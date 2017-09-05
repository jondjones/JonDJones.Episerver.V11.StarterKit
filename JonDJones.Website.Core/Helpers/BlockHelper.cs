namespace JonDJones.Website.Core.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using EPiServer.Core;
    using JonDJones.Website.Core.Extensions;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class BlockHelper : IBlockHelper
    {
        private readonly IWebsiteDependencies _websiteDependencies;

        public BlockHelper(IWebsiteDependencies _websiteDependencies)
        {
            Guard.ValidateObject(_websiteDependencies);
            _websiteDependencies = _websiteDependencies;
        }

        public IEnumerable<T> GetContentsOfType<T>(ContentArea contentArea)
        {
            return contentArea.IfNotDefault(ca => ca.Items.EmptyIfNull())
                .EmptyIfNull()
                .Select(item => _websiteDependencies.ContentRepository.Get<IContentData>(item.ContentLink))
                .OfType<T>();
        }
    }
}
