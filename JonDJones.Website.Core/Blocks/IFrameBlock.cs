namespace JonDJones.Website.Core.Blocks
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    using JonDJones.Website.Core.Blocks.Base;

    [ContentType(
        DisplayName = "IFrame Block",
        GUID = "7878c93f-8cb6-4e53-ac4a-72de08984ac4",
        Description = "IFrame Block")]
    public class IFrameBlock : GlobalBaseBlock
    {
        [CultureSpecific]
        [Display(
            Name = "Link url",
            Description = "Enter the link url",
            Order = 10,
            GroupName = SystemTabNames.Content)]
        public virtual Url Hyperlink { get; set; }
    }
}