namespace JonDJones.Website.Core.Pages.Base
{
    using EPiServer.Core;
    using JonDJones.Website.Interfaces;

    /// <summary>
    ///     Meta Base Page. Base page for all the meta related pages
    /// </summary>
    /// <seealso cref="PageData" />
    public abstract class MetaBasePage : PageData, IIsModified
    {
    }
}