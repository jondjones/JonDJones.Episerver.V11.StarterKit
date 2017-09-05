namespace JonDJones.Website.Core.Pages.Base
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Framework.DataAnnotations;
    using EPiServer.Web;
    using Geta.SEO.Sitemaps.SpecializedProperties;

    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Resources;

    public abstract class GlobalBasePage : PageData, IPageMetaDataProperties
    {
        [Display(
            Name = "Page Title",
            Description = "Page Title",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [CultureSpecific]
        public virtual string PageTitle { get; set; }

        [Display(
            Name = "Meta Title",
            GroupName = GlobalConstants.TabNames.MetaData,
            Order = 2000)]
        [Required]
        public virtual string SeoTitle { get; set; }

        [Display(
            Name = "Meta Keywords",
            GroupName = GlobalConstants.TabNames.MetaData,
            Order = 2010)]
        [Required]
        public virtual string Keywords { get; set; }

        [Display(
            Name = "Meta Description",
            GroupName = GlobalConstants.TabNames.MetaData,
            Order = 2020)]
        [Required]
        public virtual XhtmlString Description { get; set; }

        [Display(
            Name = "SEO Sitemaps",
            GroupName = GlobalConstants.TabNames.MetaData,
            Order = 2030)]
        [UIHint(UIHint.Legacy, PresentationLayer.Edit)]
        [BackingType(typeof(PropertySEOSitemaps))]
        public virtual string SeoSitemaps { get; set; }
    }
}
