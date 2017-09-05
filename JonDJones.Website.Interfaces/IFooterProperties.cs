namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IFooterProperties
    {
        XhtmlString FooterText { get; set; }

        string CopyRightNotice { get; set; }
    }
}