namespace JonDJones.Website.Interfaces
{
    public interface IMetaDataViewModel
    {
        IPageMetaDataProperties Current { get; }

        bool HasTitle { get; }
    }
}
