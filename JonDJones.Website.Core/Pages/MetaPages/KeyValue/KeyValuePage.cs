namespace JonDJones.Website.Core.Pages.MetaPages.KeyValue
{
    #region Using

    using System.ComponentModel.DataAnnotations;

    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    using JonDJones.Website.Core.Pages.Base;

    #endregion

    [ContentType(DisplayName = "Key Value Page", GUID = "c3fb4e8f-f9aa-4147-b75e-d417b9d06989",
        Description = "Key Value Page")]
    public class KeyValuePage : MetaBasePage
    {
        #region Public Properties

        [CultureSpecific]
        [Display(Name = "Text", Description = "Text", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string Text { get; set; }

        [CultureSpecific]
        [Display(Name = "Value", Description = "Value", GroupName = SystemTabNames.Content, Order = 30)]
        public virtual string Value { get; set; }

        #endregion
    }
}