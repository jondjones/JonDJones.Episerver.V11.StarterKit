namespace TSC.Fixtures.Fixtures.Pages
{
    #region Using

    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using EPiServer.Core;
    using EPiServer.ServiceLocation;

    using TSC.Fixtures.Fixtures.Base;
    using TSC.Fixtures.Resources;
    using TSC.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using TSC.Website.Core.Entities;
    using TSC.Website.Core.Pages.MetaPages.Account;
    using TSC.Website.Core.Pages.MetaPages.KeyValue;
    using TSC.Website.Interfaces;
    using TSC.Website.Interfaces.Enums;
    using TSC.Website.Shared.Helpers;

    #endregion

    public class AccountOpeningSettingsPageFixtures : FixturePageBase
    {
        #region Constructors and Destructors

        public AccountOpeningSettingsPageFixtures(
            IWebsiteDependencies websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            IContent homepage) : base(websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        public Injected<ICollectionService> CollectionService { get; set; }

        #endregion

        #region Public Methods

        public AccountOpeningSettingsPage CreatePage(string pageName, ContentReference parentPageReference)
        {
            Guard.ValidateObject(parentPageReference);

            var accountOpeningSettingsPages =
                EpiserverContentRepositories.AccountOpeningSettingsPageRepository.GetChildren(parentPageReference).ToList();

            if (accountOpeningSettingsPages.Any(x => x.Name == pageName))
            {
                return accountOpeningSettingsPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = EpiserverContentRepositories.AccountOpeningSettingsPageRepository.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            ////TODO: Jon D Jones will modify this later
            newPage.AdultTitleList = GetCollection(CollectionListEnum.AdultTitle);
            newPage.CountryList = GetCollection(CollectionListEnum.Country);
            newPage.NationalityList = GetCollection(CollectionListEnum.Nationality);
            newPage.ChildTitleList = GetCollection(CollectionListEnum.ChildTitle);

            EpiserverContentRepositories.AccountOpeningSettingsPageRepository.Save(newPage);

            return newPage;
        }

        private List<DictionaryItem> GetCollection(CollectionListEnum collectionListEnum)
        {
            var dictionaryItemList = new List<DictionaryItem>();

            var list = this.CollectionService.Service.GetCollection(collectionListEnum);

            if (list != null && list.Any())
            {
                dictionaryItemList.AddRange(list.Select(keyValuePair => new DictionaryItem { Key = keyValuePair.Key, Value = keyValuePair.Value }));
            }

            return dictionaryItemList;
        }

        #endregion
    }
}