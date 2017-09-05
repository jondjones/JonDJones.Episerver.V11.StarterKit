namespace JonDJones.Website.Core.Repository
{
    using System;
    using System.Collections.Generic;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class SiteSettingsPageRepository : ISiteSettingsPageRepository
    {
        private readonly IContentRepository _contentRepository;

        private readonly IContextResolver _contextResolver;

        private readonly IStartPageRepository _startPageRepository;

        public SiteSettingsPageRepository(IContentRepository contentRepository, IContextResolver contextResolver, IStartPageRepository startPageRepository)
        {
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(contextResolver);
            Guard.ValidateObject(startPageRepository);
            _contentRepository = contentRepository;
            _contextResolver = contextResolver;
            _startPageRepository = startPageRepository;
        }

        public SiteSettingsPage SiteSettingsPage
        {
            get
            {
                if (_startPageRepository.StartPage != null && !ContentReference.IsNullOrEmpty(_startPageRepository.StartPage.SiteSettingsPage))
                {
                    return _contentRepository.Get<SiteSettingsPage>(_startPageRepository.StartPage.SiteSettingsPage);
                }

                return null;
            }
        }

        public IEnumerable<SiteSettingsPage> GetChildren(ContentReference parentPageReference)
        {
            return _contentRepository.GetChildren<SiteSettingsPage>(parentPageReference);
        }

        public SiteSettingsPage CreateNewEmptyPage(ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);
            return _contentRepository.GetDefault<SiteSettingsPage>(parentPageReference);
        }

        public SiteSettingsPage Get(int id)
        {
            return _contentRepository.Get<SiteSettingsPage>(new ContentReference(id));
        }

        public SiteSettingsPage Get(string id)
        {
            return _contentRepository.Get<SiteSettingsPage>(new ContentReference(id));
        }

        public bool Save(SiteSettingsPage startPage)
        {
            if (startPage == null)
            {
                return false;
            }

            var isPageModified = _contextResolver.IsPageModified(startPage);
            if (!isPageModified)
            {
                return true;
            }

            _contentRepository.Save(startPage, SaveAction.Publish, AccessLevel.NoAccess);
            return true;
        }

        public void Dispose()
        {
        }
    }
}