using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace JonDJones.Website.Interfaces
{
    public interface IRedisAppSettings
    {
        string RedisHost { get; }

        string RedisPassword { get; }

        int RedisPort { get; }

        bool RedisUseSSL { get; }

        bool RedisEnableLogging { get; }
    }
}
