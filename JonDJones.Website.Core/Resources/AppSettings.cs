namespace JonDJones.Website.Core.Resources
{
    using System.Configuration;

    public static class AppSettings
    {
        public static bool RunFixtures => GetBool("RunFixtures");

        public static bool InDevelopmentMode => GetBool("InDevelopmentMode");

        private static bool GetBool(string boolToConvert)
        {
            if (string.IsNullOrEmpty(boolToConvert))
            {
                return false;
            }

            var appSetting = GetAppSetting(boolToConvert);

            bool returnValue;
            return bool.TryParse(appSetting, out returnValue) && returnValue;
        }

        private static int GetInt(string intToConvert)
        {
            if (string.IsNullOrEmpty(intToConvert))
            {
                return default(int);
            }

            var appSetting = GetAppSetting(intToConvert);

            int returnValue;
            return int.TryParse(appSetting, out returnValue) ? returnValue : default(int);
        }

        private static string GetAppSetting(string appSettingName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[appSettingName] ?? string.Empty;
        }

        public static class Redis
        {
            public static string RedisHost => GetAppSetting("RedisHost");

            public static string RedisPassword => GetAppSetting("RedisPassword");

            public static int RedisPort => GetInt("RedisPort");

            public static bool RedisUseSSL => GetBool("RedisUseSSL");

            public static bool RedisEnableLogging => GetBool("RedisEnableLogging");
        }
    }
}
