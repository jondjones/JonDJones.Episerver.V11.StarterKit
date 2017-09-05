namespace JonDJones.Website.Core.Pages
{
    #region Using

    using EPiServer.DataAnnotations;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Core.Validation.Attributes;
    using JonDJones.Website.Shared.Resources;

    #endregion

    [ContentType(
        DisplayName = "Search Page",
        GUID = "DA7B6B62-45E8-448E-96E5-A81898F4BEF1",
        Description = "Search Page",
        GroupName = GlobalConstants.GroupNames.Standard)]
    [AllowedInstances(1, Scope = AllowedInstancesAttribute.InstanceScope.Site)]
    public class SearchPage : GlobalBasePage
    {
    }
}