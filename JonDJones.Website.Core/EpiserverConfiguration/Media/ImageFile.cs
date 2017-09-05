namespace JonDJones.Website.Core.EpiserverConfiguration.Media
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Framework.DataAnnotations;

    using JonDJones.Website.Interfaces;

    [ContentType]
    [MediaDescriptor(ExtensionString = "jpg, jpeg, jpe, gif, bmp, png, svg")]
    public class ImageFile : ImageData, IImageSize
    {
        [CultureSpecific]
        [Display(Name = "Display Height", Order = 250, Description = "Display Height", GroupName = SystemTabNames.Content)]
        public virtual int DisplayHeight { get; set; }

        [CultureSpecific]
        [Display(Name = "Display Width", Order = 200, Description = "Display Width", GroupName = SystemTabNames.Content)]
        public virtual int DisplayWidth { get; set; }

        [CultureSpecific]
        [Display(Name = "Alternate Text", Order = 100, Description = "Alternate Text", GroupName = SystemTabNames.Content)]
        public virtual string AlternativeText { get; set; }

        [CultureSpecific]
        [Display(Name = "Title", Order = 150, Description = "Title", GroupName = SystemTabNames.Content)]
        public virtual string Title { get; set; }
    }
}