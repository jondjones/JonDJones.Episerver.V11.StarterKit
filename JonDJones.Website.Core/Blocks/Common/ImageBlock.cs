using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace JonDJones.Website.Core.Blocks.Common
{
    [ContentType(
        GUID = "87aa8cbf-854e-42cc-9990-27723f99c607",
        DisplayName = "Image Block",
        Description = "Image Block.",
        AvailableInEditMode = false)]
    public class ImageBlock : BlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Image",
            Description = "Image.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [UIHint(UIHint.Image)]
        [Required]
        public virtual ContentReference Image { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Alternative Text",
            Description = "Alternative Text.",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [Required]
        public virtual string AlternativeText { get; set; }
    }
}