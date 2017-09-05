namespace JonDJones.Website.Core.Pages.MetaPages.Menu
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Shared.Resources;

    [ContentType(DisplayName = "Menu Item Page", 
        GUID = "399670e8-454e-4fd9-be85-6501030d971e",
        Description = "Menu Item Page", 
        GroupName = GlobalConstants.TabNames.Navigation)]
    public class MenuItemPage : MetaBasePage
    {
        [CultureSpecific]
        [Display(
            Name = "Menu Url", 
            GroupName = SystemTabNames.Content, 
            Order = 100)]
        public virtual Url LinkUrl { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Menu Title", 
            GroupName = SystemTabNames.Content, 
            Order = 200)]
        [Required]
        public virtual string MenuTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Display Sub Menu", 
            GroupName = SystemTabNames.Content, 
            Order = 300)]
        public virtual string DisplaySubMenu { get; set; }
    }
}