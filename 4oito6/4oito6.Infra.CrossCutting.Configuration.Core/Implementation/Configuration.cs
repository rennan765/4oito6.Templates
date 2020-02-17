using _4oito6.Infra.CrossCutting.Configuration.Core.Interfaces;
using System;

namespace _4oito6.Infra.CrossCutting.Configuration.Core.Implementation
{
    public abstract class Configuration : IConfiguration
    {
        public string DbConnectionString => Environment.GetEnvironmentVariable("DbConnectionString");

        public string CacheConnectionString => Environment.GetEnvironmentVariable("CacheConnectionString");
    }
}