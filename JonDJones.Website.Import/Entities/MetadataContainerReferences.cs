namespace JonDJones.Fixtures.Entities
{
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.Pages.MetaPages;
    using JonDJones.Website.Shared.Helpers;

    using Website.Core.Pages.MetaPages.Menu;

    public class MetadataContainerReferences
    {
        public MetadataContainerReferences(SiteSettingsPage siteSettingsPage, MetaContainerPage metaContainerPage)
        {
            Guard.ValidateObject(siteSettingsPage);
            Guard.ValidateObject(metaContainerPage);

            SettingsPage = siteSettingsPage;
            MetaContainerPage = metaContainerPage;
        }
        
        public SiteSettingsPage SettingsPage { get; set; }

        public MetaContainerPage MetaContainerPage { get; set; }

        public MenuContainerPage MenuContainerPage { get; set; }
    }
}
