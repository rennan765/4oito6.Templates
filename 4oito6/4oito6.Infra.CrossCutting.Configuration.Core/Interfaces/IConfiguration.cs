namespace _4oito6.Infra.CrossCutting.Configuration.Core.Interfaces
{
    public interface IConfiguration
    {
        string DbConnectionString { get; }
        string CacheConnectionString { get; }
    }
}