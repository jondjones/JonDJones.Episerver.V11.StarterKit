namespace JonDJones.Website.Core.Pages.Base
{
    #region Using

    using EPiServer.Core;

    using JonDJones.Website.Interfaces;

    #endregion

    /// <summary>
    ///     Container Page
    /// </summary>
    /// <seealso cref="PageData" />
    /// <seealso cref="IContainerPage" />
    public abstract class ContainerPage : PageData, IContainerPage
    {
    }
}