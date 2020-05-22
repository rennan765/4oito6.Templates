using _4oito6.AuditTrail.Application.Contracts;
using _4oito6.AuditTrail.Domain.Services.Contract.Arguments;
using _4oito6.AuditTrail.Domain.Services.Contract.Interfaces;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Implementation.Base;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Application.Implementation
{
    public class AuditTrailAppService : AppServiceBase, IAuditTrailAppService
    {
        private readonly IAuditTrailService _auditTrailService;

        public AuditTrailAppService(IUnitOfWork unit, IAuditTrailService auditTrailService)
            : base(unit, new IDisposable[] { auditTrailService })
        {
            _auditTrailService = auditTrailService ?? throw new ArgumentNullException(nameof(auditTrailService));
        }

        public async Task<ResponseMessage> CreateAsync(Exception ex)
        {
            await _auditTrailService.CreateAsync(ex, DateTime.UtcNow).ConfigureAwait(false);

            var responseMessage = new ResponseMessage
            {
                StatusCode = (int)_auditTrailService.GetStatusCode(),
                Errors = _auditTrailService.GetMessages()
            };

            return responseMessage;
        }

        public async Task<ResponseMessage<IList<AuditTrailResponse>>> GetByDateAsync(DateTime startDate, DateTime endDate)
        {
            var data = await _auditTrailService.GetByDateAsync(startDate, endDate).ConfigureAwait(false);

            return new ResponseMessage<IList<AuditTrailResponse>>
            {
                Data = data,
                StatusCode = (int)_auditTrailService.GetStatusCode(),
                TotalRows = data.Count()
            };
        }
    }
}