namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IPageMetaDataProperties
    {
        string SeoTitle { get; }

        string Keywords { get; }

        XhtmlString Description { get; }
    }
}
