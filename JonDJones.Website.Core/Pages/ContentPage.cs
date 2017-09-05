namespace JonDJones.Website.Core.Pages
{
    #region Using

    using EPiServer.DataAnnotations;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Shared.Resources;

    #endregion

    [ContentType(
        DisplayName = "Content Page",
        GUID = "8dee011f-8dbf-43ab-b4f3-211db5ceb9d5",
        Description = "Content Page",
        GroupName = GlobalConstants.GroupNames.Standard)]
    public class ContentPage : DefaultContentPageBase
    {
    }
}