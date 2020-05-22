using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Interfaces;
using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model;
using _4oito6.Infra.Data.Repositories.Mongo.Core.Implementation;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Infra.Mongo.Repositories.Implementation
{
    public class AuditTrailMongoRepository : MongoRepositoryBase<AuditTrailDto>, IAuditTrailMongoRepository
    {
        public AuditTrailMongoRepository(IMongoCollection<AuditTrailDto> collection) : base(collection)
        {
        }

        public Task RemoveAsync(string id) => Collection.DeleteOneAsync(x => x.Id == id);

        public Task UpdateAsync(string id, AuditTrailDto dto)
            => Collection.ReplaceOneAsync(x => x.Id == id, dto);
    }
}