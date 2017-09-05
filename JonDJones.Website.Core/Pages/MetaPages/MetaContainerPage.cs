namespace JonDJones.Website.Core.Pages.MetaPages
{
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Filters;

    using JonDJones.Website.Core.Pages.MetaPages.KeyValue;
    using JonDJones.Website.Core.Pages.MetaPages.Menu;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Resources;

    [ContentType(
        DisplayName = "Meta Container Page",
        Description = "Parent Container for all containers",
        GUID = "c603e231-ccdd-4557-9133-8232da08550e",
        GroupName = GlobalConstants.TabNames.Container)]
    [AvailableContentTypes(
        Include = new[]
        {
            typeof(MenuContainerPage),
            typeof(MetaContainerPage),
            typeof(KeyValueContainerPage),
            typeof(SiteSettingsPage)
        })]
    public class MetaContainerPage : PageData, IContainerPage
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            this[MetaDataProperties.PageChildOrderRule] = FilterSortOrder.Index;
        }
    }
}