namespace JonDJones.Website.Interfaces
{
    using System;

    public interface ICacheManager
    {
        void DeleteValue(string key);

        string GetValue(string key);

        void StoreString(string key, string value);

        void StoreString(string key, string value, TimeSpan timeInterval);

        string GetStringValue(string key);
    }
}
