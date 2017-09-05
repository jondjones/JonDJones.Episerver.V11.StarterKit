namespace JonDJones.Website.Core.Pages
{
    using EPiServer.DataAnnotations;
    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Core.Validation.Attributes;
    using JonDJones.Website.Shared.Resources;

    [ContentType(
        DisplayName = "Page Not Found",
        GUID = "8409FE3B-4231-426E-A7EF-B21EB2C4AB75",
        Description = "Page Not Found",
        GroupName = GlobalConstants.GroupNames.Standard)]
    [AllowedInstances(1, Scope = AllowedInstancesAttribute.InstanceScope.Site)]
    public class PageNotFoundPage : DefaultContentPageBase
    {
    }
}