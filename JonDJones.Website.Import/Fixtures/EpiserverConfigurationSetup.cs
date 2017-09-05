namespace JonDJones.Fixtures.Fixtures
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Castle.Components.DictionaryAdapter;

    using EPiServer.Core;
    using EPiServer.Web;

    using JonDJones.Website.Shared.Helpers;

    #endregion

    public static class EpiserverConfigurationSetup
    {
        public static bool CreateSite(
            ISiteDefinitionRepository siteDefinitionRepository,
            IContent homePage,
            string siteUrl)
        {
            Guard.ValidateObject(siteDefinitionRepository);

            if (siteDefinitionRepository.List().Any())
            {
                return true;
            }

            var siteDefinition = new SiteDefinition
                                     {
                                         Name = "JonDJones.Website",
                                         SiteUrl = new Uri(siteUrl),
                                         StartPage = homePage.ContentLink,
                                         Hosts = GetHostDefinitions()
                                     };

            siteDefinitionRepository.Save(siteDefinition);
            return true;
        }

        private static List<HostDefinition> GetHostDefinitions()
        {
            return new EditableList<HostDefinition>
                       {
                           new HostDefinition
                               {
                                   Name = "*",
                                   Language = new CultureInfo("en")
                               },
                           new HostDefinition
                               {
                                   Name = "localhost:51611",
                                   Language = new CultureInfo("en")
                               },
                           new HostDefinition
                               {
                                   Name = "jondjones.local",
                                   Language = new CultureInfo("en")
                               }
                       };
        }
    }
}