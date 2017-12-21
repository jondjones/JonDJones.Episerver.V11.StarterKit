namespace JonDJones.Website.Core.ViewModel.Shared
{
    using System.Collections.Generic;

    using JonDJones.Website.Core.Entities;
    using JonDJones.Website.Interfaces;

    public class LayoutViewModel : ILayoutViewModel
    {
        public string SiteName { get; set; }

        public IMetaDataViewModel MetaDataProperties { get; set; }

        public IFooterViewModel FooterProperties { get; set; }

        public IHeaderViewModel HeaderProperties { get; set; }
    }
}