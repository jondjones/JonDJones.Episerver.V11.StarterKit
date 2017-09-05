namespace JonDJones.Website.Core.ViewModel.Shared
{
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class HeaderViewModel : IHeaderViewModel
    {
        public HeaderViewModel(IHeaderProperties headerProperties)
        {
            Guard.ValidateObject(headerProperties);
            Current = headerProperties;
        }

        public IHeaderProperties Current { get; }

        public ISiteSettingsProperties SiteSettingsProperties { get; set; }

        public IHeaderMenuProperties MenuProperties { get; set; }
    }
}