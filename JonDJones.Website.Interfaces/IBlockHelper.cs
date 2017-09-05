namespace JonDJones.Website.Interfaces
{
    using System.Collections.Generic;

    using EPiServer.Core;

    public interface IBlockHelper
    {
        IEnumerable<T> GetContentsOfType<T>(ContentArea contentArea);
    }
}
