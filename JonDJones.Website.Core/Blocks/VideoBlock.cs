namespace JonDJones.Website.Core.Blocks
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    using JonDJones.Website.Core.Blocks.Base;

    [ContentType(DisplayName = "Video Block", GUID = "5e59d770-fe74-423d-8539-4e121bb124c6", Description = "Video Block")]
    public class VideoBlock : GlobalBaseBlock
    {
        [CultureSpecific]
        [Display(
            Name = "Auto Play",
            Description = "Select if the video play by auto, by default it is false",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual bool AutoPlay { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Show Related Videos",
            Description = "Select if the related videos needs to be displayed, by default it is false",
            GroupName = SystemTabNames.Content,
            Order = 15)]
        public virtual bool ShowRelatedVideos { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Video Url",
            Description = "Enter the youtube embed video url",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual Url VideoUrl { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            AutoPlay = false;
            ShowRelatedVideos = false;
        }
    }
}