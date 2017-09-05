namespace JonDJones.Website.Interfaces
{
    using System;
    using System.Collections.Generic;

    using EPiServer.Core;

    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity CreateNewEmptyPage(ContentReference parentPageReference);

        IEnumerable<TEntity> GetChildren(ContentReference parentPageReference);

        TEntity Get(int id);

        TEntity Get(string id);

        bool Save(TEntity model);
    }
}
