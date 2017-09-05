namespace JonDJones.Website.Core.Pages
{
    using System.ComponentModel.DataAnnotations;
    
    using EPiServer.Core;
    using EPiServer.DataAnnotations;
    using EPiServer.Web;

    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Core.Pages.MetaPages.Menu;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Resources;

    [ContentType(
        DisplayName = "Start Page", 
        GUID = "6671aa96-0a1b-4618-88e3-c98e1a78dcb4", 
        Description = "Start Page",
        GroupName = "Standard")]
    public class StartPage : DefaultContentPageBase, IFooterProperties, IHeaderProperties
    {
        [Ignore]
        public bool HasSiteSettingsPage => !PageReference.IsNullOrEmpty(SiteSettingsPage);

        [Display(Name = "Site Settings Page", Description = "Sets up the Site Settings page",
            GroupName = GlobalConstants.TabNames.PageReferences, Order = 300)]
        [AllowedTypes(AllowedTypes = new[] { typeof(SiteSettingsPage) })]
        public virtual PageReference SiteSettingsPage { get; set; }



        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [Display(Name = "Logo", Description = "Sets up the Site Logo", GroupName = GlobalConstants.TabNames.Header,
            Order = 100)]
        public virtual ContentReference Logo { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [Display(Name = "Mobie Logo", Description = "Sets up the Site Mobie Logo", GroupName = GlobalConstants.TabNames.Header,
            Order = 102)]
        public virtual ContentReference MobileLogo { get; set; }

        [Display(Name = "Primary Navigation Container Page",
            Description = "Sets up the Primary Navigation container page", GroupName = GlobalConstants.TabNames.Header,
            Order = 110)]
        [AllowedTypes(AllowedTypes = new[] { typeof(MenuContainerPage) })]
        public virtual PageReference PrimaryNavigationContainerPage { get; set; }

        [CultureSpecific]
        [Display(Name = "Enable Breadcrumb", Description = "Sets up the breadcrumb to be enabled",
            GroupName = GlobalConstants.TabNames.Header, Order = 120)]
        public virtual bool EnableBreadcrumb { get; set; }


        #region Footer Settings
        [CultureSpecific]
        [Display(Name = "Footer Text",
            Description = "Footer Text",
            GroupName = GlobalConstants.TabNames.Footer,
            Order = 550)]
        public virtual XhtmlString FooterText { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Copyright Notice",
            GroupName = GlobalConstants.TabNames.Footer,
            Order = 500)]
        public virtual string CopyRightNotice { get; set; }
        #endregion
    }
}