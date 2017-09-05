namespace JonDJones.Website.Core.ViewModel.Shared
{
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class FooterViewModel : IFooterViewModel
    {
        public FooterViewModel(IFooterProperties footerProperties)
        {
            Guard.ValidateObject(footerProperties);
            Current = footerProperties;
        }

        public IFooterProperties Current { get; }
    }
}