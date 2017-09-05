namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IHeaderProperties
    {
        bool EnableBreadcrumb { get; set; }

        ContentReference Logo { get; set; }

        ContentReference MobileLogo { get; set; }
    }
}