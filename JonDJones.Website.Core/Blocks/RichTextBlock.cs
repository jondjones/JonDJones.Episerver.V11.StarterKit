namespace JonDJones.Website.Core.Blocks
{
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    [ContentType(DisplayName = "Rich Text Block",
        GUID = "EB15ECE1-341D-4F3D-887B-315963732790",
        Description = "Rich text")]
    public class RichTextBlock : BlockData
    {
        [Display(Name = "Text",
            Description = "Block content",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual XhtmlString Text { get; set; }
    }
}