using System;

namespace _4oito6.Infra.CrossCutting.Configuration.Connection
{
    public class ConnectionConfiguration : IConnectionConfiguration
    {
        public string DbConnectionString => Environment.GetEnvironmentVariable("DbConnectionString");

        public string CacheConnectionString => Environment.GetEnvironmentVariable("CacheConnectionString");

        public string CacheDbName => Environment.GetEnvironmentVariable("CacheDbName");

        public string MongoConnectionString => Environment.GetEnvironmentVariable("MongoConnectionString");

        public string MongoDbName => Environment.GetEnvironmentVariable("MongoDbName");
    }
}