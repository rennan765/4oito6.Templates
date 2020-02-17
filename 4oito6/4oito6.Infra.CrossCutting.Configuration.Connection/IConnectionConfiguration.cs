namespace _4oito6.Infra.CrossCutting.Configuration.Connection
{
    public interface IConnectionConfiguration
    {
        string DbConnectionString { get; }
        string CacheConnectionString { get; }
    }
}