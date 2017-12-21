using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace JonDJones.Website.Core.Blocks
{
    [ContentType(
        GUID = "91f5e60f-e2fc-4d1f-8126-a7a1e83030e5",
        DisplayName = "Call To Action",
        Description = "Call To Action.",
        AvailableInEditMode = false)]
    public class CallToActionBlock : BlockData
    {
        [Display(
            Name = "Link Text",
            Description = "Link Text.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [CultureSpecific]
        public virtual string CallToActionText { get; set; }

        [Display(
            Name = "Link Url",
            Description = "Link Url.",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [CultureSpecific]
        public virtual Url CallToActionLink { get; set; }
    }
}