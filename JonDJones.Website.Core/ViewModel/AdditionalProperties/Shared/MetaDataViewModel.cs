namespace JonDJones.Website.Core.ViewModel.Shared
{
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class MetaDataViewModel : IMetaDataViewModel
    {
        public MetaDataViewModel(IPageMetaDataProperties model)
        {
            Guard.ValidateObject(model);
            Current = model;
        }

        public IPageMetaDataProperties Current { get; }

        public bool HasTitle => !string.IsNullOrEmpty(Current.SeoTitle);
    }
}