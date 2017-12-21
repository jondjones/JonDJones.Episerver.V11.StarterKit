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
using JonDJones.Website.Core.ViewModel.Pages;

namespace JonDJones.Website.Core.Repository
{
    public class ContentPageService : IContentPageService
    {
        private readonly IContentRepository _contentRepository;

        private readonly IContextResolver _contextResolver;

        public ContentPageService(IContentRepository contentRepository, IContextResolver contextResolver)
        {
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(contextResolver);

            _contentRepository = contentRepository;
            _contextResolver = contextResolver;
        }

        public ContentPageAdditionalProperties GetAdditionalProperties(ContentPage startpage)
        {
            return new ContentPageAdditionalProperties();
        }

        public IEnumerable<ContentPage> GetChildren(ContentReference parentPageReference)
        {
            try
            {
                return _contentRepository.GetChildren<ContentPage>(parentPageReference);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ContentPage CreateNewEmptyPage(ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);
            return _contentRepository.GetDefault<ContentPage>(parentPageReference);
        }

        public ContentPage Get(int id)
        {
            return _contentRepository.Get<ContentPage>(new ContentReference(id));
        }

        public ContentPage Get(string id)
        {
            return _contentRepository.Get<ContentPage>(new ContentReference(id));
        }

        public bool Save(ContentPage startPage)
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