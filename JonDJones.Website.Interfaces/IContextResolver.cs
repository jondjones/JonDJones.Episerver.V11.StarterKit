namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IContextResolver
    {
        PageData CurrentPage { get; }

        PageReference StartPage { get; }

        PageReference RootPage { get; }

        ContentReference GlobalBlockFolder { get; }

        ContentFolder SiteAssetsFolder { get; }

        bool IsInEditMode { get; }

        bool IsPageModified(PageData page);

        int GetIdFromBlock(BlockData block);

        ContentArea AddContentAreaItem(ContentArea contentArea, ContentAreaItem contentAreaItem);

        bool HasContentAreaGotItems(ContentArea contentArea);
    }
}