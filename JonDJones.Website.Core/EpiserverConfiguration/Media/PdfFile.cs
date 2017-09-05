namespace JonDJones.Website.Core.EpiserverConfiguration.Media
{
    using EPiServer.Core;
    using EPiServer.DataAnnotations;
    using EPiServer.Framework.DataAnnotations;

    [ContentType]
    [MediaDescriptor(ExtensionString = "pdf")]
    public class PdfFile : MediaData
    {
    }
}
