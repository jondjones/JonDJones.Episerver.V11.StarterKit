namespace JonDJones.Website.Interfaces
{
    using System.Collections.Generic;

    using JonDJones.Website.Interfaces.Enums;

    public interface ICollectionService
    {
        Dictionary<string, string> GetCollection(CollectionListEnum collectionListEnum);
    }
}