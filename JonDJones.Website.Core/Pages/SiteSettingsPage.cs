namespace JonDJones.Website.Core.Pages
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer.Core;
    using EPiServer.DataAnnotations;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Core.Validation.Attributes;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Resources;

    [Access(Roles = "Administrators")]
    [ContentType(
        DisplayName = "Site Settings Page",
        GUID = "5835D525-77D9-4F5F-BAB9-D0C14E273C15",
        Description = "Global Configuration Settings",
        GroupName = GlobalConstants.TabNames.Configuration)]
    [AllowedInstances(1, Scope = AllowedInstancesAttribute.InstanceScope.Site)]
    public class SiteSettingsPage : ContainerPage, ISiteSettingsProperties
    {
        [Display(
            Name = "Search Page", 
            Description = "Sets up the search page",
            GroupName = GlobalConstants.TabNames.PageReferences, 
            Order = 100)]
        [AllowedTypes(AllowedTypes = new[] { typeof(SearchPage) })]
        public virtual PageReference SearchPage { get; set; }
    }
}