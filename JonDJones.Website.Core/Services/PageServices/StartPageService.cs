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
    public class StartPageService : IStartPageService
    {
        private readonly IContentRepository _contentRepository;

        private readonly IContextResolver _contextResolver;

        public StartPageService(IContentRepository contentRepository, IContextResolver contextResolver)
        {
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(contextResolver);

            _contentRepository = contentRepository;
            _contextResolver = contextResolver;
        }

        public StartPage Homepage => ContentReference.IsNullOrEmpty(_contextResolver.StartPage)
                                          ? null
                                          : _contentRepository.Get<StartPage>(_contextResolver.StartPage);

        public StartPageAdditionalProperties GetStartPageAdditionalProperties(StartPage startpage)
        {
            return new StartPageAdditionalProperties();
        }

        public IEnumerable<StartPage> GetChildren(ContentReference parentPageReference)
        {
            try
            {
                return _contentRepository.GetChildren<StartPage>(parentPageReference);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public StartPage CreateNewEmptyPage(ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);
            return _contentRepository.GetDefault<StartPage>(parentPageReference);
        }

        public StartPage Get(int id)
        {
            return _contentRepository.Get<StartPage>(new ContentReference(id));
        }

        public StartPage Get(string id)
        {
            return _contentRepository.Get<StartPage>(new ContentReference(id));
        }

        public bool Save(StartPage startPage)
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