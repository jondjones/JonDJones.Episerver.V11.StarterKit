namespace JonDJones.Website.Core.Pages.MetaPages.KeyValue
{
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Filters;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Shared.Resources;

    [ContentType(
        DisplayName = "Key Value Container Page", 
        GUID = "763A982A-7EBF-4E9E-98E8-381F8684B5CD",
        Description = "Container for Key Value", 
        GroupName = GlobalConstants.TabNames.Container)]
    [AvailableContentTypes(Include = new[] { typeof(KeyValuePage) })]
    public class KeyValueContainerPage : ContainerPage
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            this[MetaDataProperties.PageChildOrderRule] = FilterSortOrder.Index;
        }
    }
}