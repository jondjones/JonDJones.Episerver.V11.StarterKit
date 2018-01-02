using System.ComponentModel.DataAnnotations;

using EPiServer.Core;
using EPiServer.DataAnnotations;

using JonDJones.Website.Core.Pages.Base;
using JonDJones.Website.Core.Validation.Attributes;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Resources;
using EPiServer.Web;
using EPiServer.DataAbstraction;

namespace JonDJones.Website.Core.Pages
{
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
            Name = "Logo",
            Description = "Select A Logo",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [Required]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Logo { get; set; }
    }
}