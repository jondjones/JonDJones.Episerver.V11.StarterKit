namespace JonDJones.Website.Core.Repository
{
    using System.Collections.Generic;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages.MetaPages.KeyValue;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class KeyValueRepository : IKeyValueRepository
    {
        private readonly IContentRepository _contentRepository;

        private readonly IContextResolver _contextResolver;

        public KeyValueRepository(IContentRepository contentRepository, IContextResolver contextResolver)
        {
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(contextResolver);
            _contentRepository = contentRepository;
            _contextResolver = contextResolver;
        }
        public IEnumerable<KeyValuePage> GetChildren(ContentReference parentPageReference)
        {
            return _contentRepository.GetChildren<KeyValuePage>(parentPageReference);
        }

        public KeyValuePage CreateNewEmptyPage(ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);
            return _contentRepository.GetDefault<KeyValuePage>(parentPageReference);
        }

        public KeyValuePage Get(int id)
        {
            return _contentRepository.Get<KeyValuePage>(new ContentReference(id));
        }

        public KeyValuePage Get(string id)
        {
            return _contentRepository.Get<KeyValuePage>(new ContentReference(id));
        }

        public bool Save(KeyValuePage keyValuePage)
        {
            if (keyValuePage == null)
            {
                return false;
            }

            var isPageModified = _contextResolver.IsPageModified(keyValuePage);

            if (!isPageModified)
            {
                return true;
            }

            _contentRepository.Save(keyValuePage, SaveAction.Publish, AccessLevel.NoAccess);
            return true;
        }

        public void Dispose()
        {
        }
    }
}