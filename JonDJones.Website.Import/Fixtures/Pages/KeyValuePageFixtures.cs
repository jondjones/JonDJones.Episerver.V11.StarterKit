using System.Linq;

using EPiServer.Core;

using JonDJones.Fixtures.Fixtures.Base;
using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.Core.Pages.MetaPages.KeyValue;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Fixtures.Fixtures.Pages
{
    public class KeyValuePageFixtures : FixturePageBase
    {
        public KeyValuePageFixtures(
            IWebsiteDependencies _websiteDependencies,
            IPageTypeServices pagetypeServices,
            IContent homepage)
            : base(_websiteDependencies, pagetypeServices, homepage)
        {
        }

        public KeyValuePage CreatePage(string pageName, ContentReference parentPageReference, string text, string value)
        {
            Guard.ValidateObject(parentPageReference);

            var keyValuePages =
                PageTypeServices.KeyValueService.GetChildren(parentPageReference).ToList();

            if (keyValuePages.Any(x => x.Name == pageName))
            {
                return keyValuePages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = PageTypeServices.KeyValueService.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            newPage.Text = pageName;
            newPage.Value = value;

            PageTypeServices.KeyValueService.Save(newPage);

            return newPage;
        }
    }
}