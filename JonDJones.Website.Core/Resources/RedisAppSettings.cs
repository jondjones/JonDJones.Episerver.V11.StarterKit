using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Website.Core.Dependencies
{
    public class RedisAppSettings : IRedisAppSettings
    {
        private readonly IAppSettingsService _appSettingsService;

        public RedisAppSettings(IAppSettingsService appSettingsService)
        {
            Guard.ValidateObject(appSettingsService);
            _appSettingsService = appSettingsService;
        }

        public string RedisHost => _appSettingsService.GetAppSetting("RedisHost");

        public string RedisPassword => _appSettingsService.GetAppSetting("RedisPassword");

        public int RedisPort => _appSettingsService.GetInt("RedisPort");

        public bool RedisUseSSL => _appSettingsService.GetBool("RedisUseSSL");

       public bool RedisEnableLogging => _appSettingsService.GetBool("RedisEnableLogging");
    }
}
