namespace JonDJones.Website.Interfaces.Enums
{
    using JonDJones.Website.Interfaces.Attributes;

    public enum DisplayOptionEnum
    {
        Unknown,

        [BootstrapClass(Name = "col-md-12")]
        [DisplayOptionName(Name = DisplayOptionTags.FullWidth)]
        Full,

        [BootstrapClass(Name = "col-md-8")]
        [DisplayOptionName(Name = DisplayOptionTags.TwoThirdsWidth)]
        TwoThirds,

        [BootstrapClass(Name = "col-md-6")]
        [DisplayOptionName(Name = DisplayOptionTags.HalfWidth)]
        Half,

        [BootstrapClass(Name = "col-md-4")]
        [DisplayOptionName(Name = DisplayOptionTags.OneThirdWidth)]
        OneThird,

        [BootstrapClass(Name = "col-md-3")]
        [DisplayOptionName(Name = DisplayOptionTags.OneFourthWidth)]
        OneFourth
    }
}
