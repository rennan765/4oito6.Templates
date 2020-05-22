using _4oito6.AuditTrail.Domain.Services.Contract.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Application.Contracts
{
    public interface IAuditTrailAppService : IAppServiceBase
    {
        Task<ResponseMessage> CreateAsync(Exception ex);

        Task<ResponseMessage<IList<AuditTrailResponse>>> GetByDateAsync(DateTime startDate, DateTime endDate);
    }
}