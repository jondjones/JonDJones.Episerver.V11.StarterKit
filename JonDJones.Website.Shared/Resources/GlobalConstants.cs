namespace JonDJones.Website.Shared.Resources
{
    using System;

    using System.ComponentModel.DataAnnotations;

    using EPiServer.DataAnnotations;

    public static class GlobalConstants
    {
        public static class CacheProfiles
        {
            public const string Short = "Short";

            public const string Hour = "Hour";

            public const string Day = "Day";
        }

        public static class DateFormats
        {
            public const string Standard = "dd MMMM yyyy";
        }

        public static class GroupNames
        {
            public const string Standard = "Standard";

            public const string Accounts = "Accounts";

            public const string Functional = "Functional";
        }

        [GroupDefinitions]
        public static class TabNames
        {
            [Display(Order = 100)]
            public const string Header = "Header Settings";

            [Display(Order = 200)]
            public const string Footer = "Footer Settings";

            [Display(Order = 300)]
            public const string Navigation = "Navigation";

            [Display(Order = 400)]
            public const string PageReferences = "Page References";

            [Display(Order = 500)]
            public const string Container = "Container";

            [Display(Order = 600)]
            public const string Configuration = "Configuration";

            [Display(Order = 700)]
            public const string Buttons = "Buttons";

            [Display(Order = 800)]
            public const string Backgrounds = "Backgrounds";

            [Display(Order = 900)]
            public const string AdditionalDetails = "Additional Details";

            [Display(Order = 1000)]
            public const string MetaData = "Metadata Settings";
        }

        public static class PartialViews
        {
            public const string Header = "Header";

            public const string Footer = "Footer";
        }

        public static class Views
        {
            public const string Index = "Index";
        }

        public static class Urls
        {
            public const string Root = "~/";
        }

        public static class ViewBagProperties
        {
            public const string Childrencustomtagname = "childrencustomtagname";

            public const string Childrencssclass = "childrencssclass";

            public const string Tag = "tag";

            public const string RenderSettings = "renderSettings";
        }

        public static class ViewDataProperties
        {
            public const string CssClassName = "class";
        }

        public static class UIHints
        {
            public const string StringList = "StringList";
        }

        public static class Video
        {
            public const int On = 1;

            public const int Off = 0;
        }

        public static class Keys
        {
            public const string SiteAssetsFolderName = "Assets";
        }

        public static class FileType
        {
            public const string Png = ".png";
        }
    }
}
