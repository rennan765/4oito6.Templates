using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model;
using _4oito6.Infra.Data.Repositories.Core.Contracts;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Interfaces
{
    public interface IAuditTrailMongoRepository : IMongoRepositoryBase<AuditTrailDto>
    {
        Task UpdateAsync(string id, AuditTrailDto dto);

        Task RemoveAsync(string id);
    }
}