using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model;
using _4oito6.Infra.Data.Repositories.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Interfaces
{
    public interface IAuditTrailMongoRepository : IMongoRepositoryBase<AuditTrailDto>
    {
        Task<IList<AuditTrailDto>> GetByDateAsync(DateTime startDate, DateTime endDate);

        Task UpdateAsync(string id, AuditTrailDto dto);

        Task RemoveAsync(string id);
    }
}