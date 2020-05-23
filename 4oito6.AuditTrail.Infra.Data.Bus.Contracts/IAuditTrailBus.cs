using _4oito6.Infra.Data.Bus.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Infra.Data.Bus.Contracts
{
    public interface IAuditTrailBus : IBusBase
    {
        Task CreateAsync(Domain.Entities.AuditTrail auditTrail);

        Task<IList<Domain.Entities.AuditTrail>> GetByDateAsync(DateTime startDate, DateTime endDate);
    }
}