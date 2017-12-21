namespace JonDJones.Website.Core.ViewModel.Factory
{
    using System.Collections.Generic;
    using System.Linq;

    using EPiServer.Core;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Entities;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Core.ViewModel.Factory.Interfaces;
    using JonDJones.Website.Core.ViewModel.Shared;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;
    using JonDJones.Website.Shared.Resources;

    public class LayoutViewModelFactory
    {
        private readonly IPageTypeServices _episerverContentRepositories;

        private readonly IHeaderViewModelFactory _headerViewModelFactory;

        public LayoutViewModelFactory(
            IWebsiteDependencies websiteDependencies,
            IPageTypeServices episerverContentRepositories,
            IHeaderViewModelFactory headerViewModelFactory)
        {
            Guard.ValidateObject(websiteDependencies);
            Guard.ValidateObject(episerverContentRepositories);
            Guard.ValidateObject(headerViewModelFactory);

            _episerverContentRepositories = episerverContentRepositories;
            _headerViewModelFactory = headerViewModelFactory;
        }

        public LayoutViewModel CreateLayoutViewModel(PageData currentPage)
        {
            var homePage = _episerverContentRepositories.StartPageService.Homepage;

            var siteSettingsPage = _episerverContentRepositories.SiteSettingsService.SiteSettingsPage;
            var headerProperties = _headerViewModelFactory.CreateHeaderProperties(homePage, siteSettingsPage);

            var footerProperties = CreateFooterProperties(homePage);

            var layoutModel = new LayoutViewModel
            {
                MetaDataProperties = CreateSeoViewModel(currentPage is GlobalBasePage ? (GlobalBasePage)currentPage : homePage),
                HeaderProperties = headerProperties,
                FooterProperties = footerProperties,
                SiteName = homePage.PageName
            };

            return layoutModel;
        }

        private static MetaDataViewModel CreateSeoViewModel(IPageMetaDataProperties metaDataProperties)
        {
            return metaDataProperties == null ? null : new MetaDataViewModel(metaDataProperties);
        }

        private FooterViewModel CreateFooterProperties(StartPage startPage)
        {
            return new FooterViewModel(startPage);
        }
    }
}