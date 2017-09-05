namespace JonDJones.Website.Core.Pages.MetaPages.Menu
{
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Filters;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Shared.Resources;

    [ContentType(DisplayName = "Menu Container Page", 
        GUID = "4e53646d-e5a4-4404-b01c-12a9a1fccda6",
        Description = "A placeholder to help organise the EPiServer page tree",
        GroupName = GlobalConstants.TabNames.Container)]
    [AvailableContentTypes(Include = new[] { typeof(MenuPage) })]
    public class MenuContainerPage : ContainerPage
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            this[MetaDataProperties.PageChildOrderRule] = FilterSortOrder.Index;
        }
    }
}