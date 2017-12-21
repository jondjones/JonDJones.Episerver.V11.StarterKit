using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Website.Core.Dependencies
{
    public class GlobalAppSettings : IGlobalAppSettings
    {
        private readonly IAppSettingsService _appSettingsService;
        public GlobalAppSettings(IAppSettingsService appSettingsService)
        {
            Guard.ValidateObject(appSettingsService);
            _appSettingsService = appSettingsService;
        }

        public bool RunFixtures => _appSettingsService.GetBool("RunFixtures");

        public bool InDevelopmentMode => _appSettingsService.GetBool("InDevelopmentMode");
    }
}
