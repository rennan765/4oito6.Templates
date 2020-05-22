namespace _4oito6.Infra.CrossCutting.Configuration.Connection
{
    public interface IConnectionConfiguration
    {
        string DbConnectionString { get; }
        string CacheConnectionString { get; }
        string CacheDbName { get; }
        string MongoConnectionString { get; }
        string MongoDbName { get; }
    }
}