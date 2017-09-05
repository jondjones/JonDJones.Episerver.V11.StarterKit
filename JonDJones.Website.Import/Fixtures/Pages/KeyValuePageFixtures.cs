namespace JonDJones.Fixtures.Fixtures.Pages
{
    using System.Linq;

    using EPiServer.Core;

    using JonDJones.Fixtures.Fixtures.Base;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages.MetaPages.KeyValue;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class KeyValuePageFixtures : FixturePageBase
    {
        public KeyValuePageFixtures(
            IWebsiteDependencies _websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories,
            IContent homepage)
            : base(_websiteDependencies, episerverContentRepositories, homepage)
        {
        }

        public KeyValuePage CreatePage(string pageName, ContentReference parentPageReference, string text, string value)
        {
            Guard.ValidateObject(parentPageReference);

            var keyValuePages =
                EpiserverContentRepositories.KeyValueRepository.GetChildren(parentPageReference).ToList();

            if (keyValuePages.Any(x => x.Name == pageName))
            {
                return keyValuePages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = EpiserverContentRepositories.KeyValueRepository.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            newPage.Text = pageName;
            newPage.Value = value;

            EpiserverContentRepositories.KeyValueRepository.Save(newPage);

            return newPage;
        }
    }
}