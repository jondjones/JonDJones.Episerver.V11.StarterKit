using JonDJones.Website.Interfaces;
using System.Configuration;

namespace DeLonghi.Web.Business
{
    public class AppSettingsService : IAppSettingsService
    {
        public bool GetBool(string boolToConvert)
        {
            if (string.IsNullOrEmpty(boolToConvert))
            {
                return false;
            }

            var appSetting = GetAppSetting(boolToConvert);
            return bool.TryParse(appSetting, out bool returnValue) && returnValue;
        }

        public int GetInt(string intToConvert)
        {
            if (string.IsNullOrEmpty(intToConvert))
            {
                return default(int);
            }

            var appSetting = GetAppSetting(intToConvert);
            return int.TryParse(appSetting, out int returnValue) ? returnValue : default(int);
        }

        public string GetAppSetting(string appSettingName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[appSettingName] ?? string.Empty;
        }
    }
}