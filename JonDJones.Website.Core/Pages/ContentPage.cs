namespace JonDJones.Website.Core.Pages
{
    using EPiServer.Core;
    #region Using

    using EPiServer.DataAnnotations;
    using EPiServer.Web;
    using JonDJones.Website.Core.EpiserverConfiguration;
    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Shared.Resources;
    using System.ComponentModel.DataAnnotations;

    #endregion

    [ContentType(
        DisplayName = "Content Page",
        GUID = "8dee011f-8dbf-43ab-b4f3-211db5ceb9d5",
        Description = "Content Page",
        GroupName = GlobalConstants.GroupNames.Standard)]
    public class ContentPage : DefaultContentPageBase
    {

        [HideOnContentCreateAttribute]
        [Required]
        [Display(
            Name = "Images",
            Description = "Sets up the Site Logo",
            GroupName = GlobalConstants.TabNames.Header,
            Order = 100)]
        public virtual ContentArea ContentArea { get; set; }
    }
}