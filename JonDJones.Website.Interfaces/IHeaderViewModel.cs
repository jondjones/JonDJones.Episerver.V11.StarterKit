namespace JonDJones.Website.Interfaces
{
    public interface IHeaderViewModel
    {
        IHeaderProperties Current { get; }

        IHeaderMenuProperties MenuProperties { get; set; }

        ISiteSettingsProperties SiteSettingsProperties { get; set; }
    }
}