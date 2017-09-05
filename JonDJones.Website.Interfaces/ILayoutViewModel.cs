namespace JonDJones.Website.Interfaces
{
    public interface ILayoutViewModel
    {
        #region Public Properties

        IFooterViewModel FooterProperties { get; set; }

        IHeaderViewModel HeaderProperties { get; set; }

        IMetaDataViewModel MetaDataProperties { get; set; }

        string SiteName { get; set; }

        #endregion
    }
}