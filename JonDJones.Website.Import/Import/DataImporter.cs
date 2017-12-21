using EPiServer.Core;
using EPiServer.Enterprise;
using EPiServer.Logging.Compatibility;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;
using System;
using System.Configuration;
using System.IO;

namespace JonDJones.Fixtures.Import
{
    public class EpiserverDataImporter
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(EpiserverDataImporter));

        private readonly IDataImporter _dataImporter;

        private readonly IWebsiteDependencies _websiteDependencies;

        public EpiserverDataImporter(
            IDataImporter dataImporter,
            IWebsiteDependencies websiteDependencies)
        {
            Guard.ValidateObject(dataImporter);
            Guard.ValidateObject(websiteDependencies);

            _websiteDependencies = websiteDependencies;
            _dataImporter = dataImporter;
        }

        public bool ImportContentOnToRootPage()
        {
            var episerverdataLocation = GetAppDataFileLocation();
            var rootPage = _websiteDependencies.ContextResolver.RootPage;

            ImportFilePackage(rootPage, episerverdataLocation);
            return true;
        }

        public bool ImportFilePackage(
            ContentReference startNode,
            string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    _log.Error($"Can't find the import file in {filePath}");
                    return false;
                }

                var fileStream = new FileStream(filePath, FileMode.Open);
                var options = ImportOptions.DefaultOptions;
                options.KeepIdentity = true;
                options.ValidateDestination = true;
                options.EnsureContentNameUniqueness = false;
                options.IsTest = false;

                _dataImporter.Import(fileStream, startNode, options);
            }
            catch (Exception ex)
            {
                _log.Error("Can't import data because, ", ex);
                return false;
            }

            return true;
        }

        private string GetAppDataFileLocation()
        {
            return $@"{EPiServer.Framework.Configuration.EPiServerFrameworkSection.Instance.AppData.BasePath}\{ConfigurationManager.AppSettings["ExportFilename"]}";
        }
    }
}