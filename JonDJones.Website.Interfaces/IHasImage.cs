namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IHasImage
    {
        ContentReference Image { get; set; }
    }
}