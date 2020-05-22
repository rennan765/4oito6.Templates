using _4oito6.AuditTrail.Domain.Services.Contract.Arguments;
using _4oito6.Domain.Services.Core.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Domain.Services.Contract.Interfaces
{
    public interface IAuditTrailService : IServiceBase
    {
        Task CreateAsync(Exception ex);

        Task CreateAsync(Exception ex, DateTime date);

        Task<IList<AuditTrailResponse>> GetByDateAsync(DateTime startDate, DateTime endDate);
    }
}