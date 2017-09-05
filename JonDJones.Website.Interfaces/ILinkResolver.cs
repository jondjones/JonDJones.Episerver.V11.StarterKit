namespace JonDJones.Website.Interfaces
{
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.SpecializedProperties;

    public interface ILinkResolver
    {
        string GetFriendlyUrl(ContentReference pageLink);

        string GetFriendlyUrl(Url internalUrl);

        string ResolveUrl(string originalString);

        string ResolveUrl(IContent content);

        string ResolveUrl(Url url);

        string ResolveUrl(ContentReference contentReference);

        string ResolveUrl(LinkItem linkItem);
    }
}