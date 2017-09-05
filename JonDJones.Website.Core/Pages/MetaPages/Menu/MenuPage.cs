namespace JonDJones.Website.Core.Pages.MetaPages.Menu
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Web;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Shared.Resources;

    [ContentType(DisplayName = "Menu Page",
        GUID = "8b977a97-7cfd-4419-935d-37febcd7e7d2",
        Description = "Menu Page",
        GroupName = GlobalConstants.TabNames.Navigation)]
    [AvailableContentTypes(Include = new[] { typeof(MenuItemPage) })]
    public class MenuPage : MetaBasePage
    {
        [CultureSpecific]
        [Display(
            Name = "Menu Url", 
            Description = "Menu Url", 
            GroupName = SystemTabNames.Content, 
            Order = 50)]
        public virtual Url MenuContentPage { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Main Menu Title", 
            Description = "Main Menu Title", 
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [Required]
        public virtual string MainMenuTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Menu Image", 
            Description = "Menu Image", 
            GroupName = SystemTabNames.Content, 
            Order = 200)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MenuImageUrl { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Sub Menu Title", 
            Description = "Sub Menu Title", 
            GroupName = SystemTabNames.Content,
            Order = 300)]
        public virtual string SubMenuTitle { get; set; }
    }
}