namespace JonDJones.Website.Core.Dependencies
{
    using System;

    using EPiServer.Logging.Compatibility;

    using StackExchange.Redis;

    using JonDJones.Website.Core.Resources;
    using JonDJones.Website.Interfaces;

    public class CacheManager : ICacheManager
    {
        private static readonly ILog Logger = LogManager.GetLogger("Legacy");

        private static readonly IDatabase Database;

        static CacheManager()
        {
            var redisHost = AppSettings.Redis.RedisHost;
            var redisPort = AppSettings.Redis.RedisPort;
            var useSsl = AppSettings.Redis.RedisUseSSL;
            var redisPassword = AppSettings.Redis.RedisPassword;

            var connectionString = $"{redisHost}:{redisPort}";

            if (!string.IsNullOrEmpty(redisPassword))
            {
                connectionString = $"{connectionString},password={redisPassword},ssl={useSsl}";
            }

            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            Database = connectionMultiplexer.GetDatabase();
        }

        public void DeleteValue(string key)
        {
            try
            {
                key = key.Replace(" ", string.Empty);
                Database.KeyDelete(key);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to delete {key} from Redis", ex);
            }
        }

        public string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var returnValue = string.Empty;

            try
            {
                var valueAsString = Database.StringGet(key);
                returnValue = valueAsString;
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to read {key} from Redis", ex);
            }

            return returnValue;
        }

        public void StoreString(string key, string value)
        {
            Database.StringSet(key, value);
        }

        public void StoreString(string key, string value, TimeSpan timeInterval)
        {
            Database.StringSet(key, value, timeInterval);
        }

        public string GetStringValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            try
            {
                string valueAsString = Database.StringGet(key);

                return !string.IsNullOrEmpty(valueAsString)
                    ? valueAsString.Trim()
                    : string.Empty;
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to read {key} from Redis", ex);
            }

            return string.Empty;
        }
    }
}