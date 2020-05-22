using _4oito6.AuditTrail.Domain.Services.Contract.Arguments;
using _4oito6.AuditTrail.Domain.Services.Contract.Interfaces;
using _4oito6.AuditTrail.Domain.Specs;
using _4oito6.AuditTrail.Infra.CrossCutting.Messages;
using _4oito6.AuditTrail.Infra.Data.Bus.Contracts;
using _4oito6.Domain.Services.Core.Implementation.Base;
using _4oito6.Domain.Specs.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Domain.Services.Implementation
{
    public class AuditTrailService : ServiceBase, IAuditTrailService
    {
        private readonly IAuditTrailBus _bus;

        public AuditTrailService(IAuditTrailBus bus)
            : base(new IDisposable[] { bus })
        {
            _bus = bus ?? throw new NotImplementedException(nameof(bus));
        }

        public async Task CreateAsync(Exception ex, DateTime date)
        {
            if (ex == null)
            {
                var spec = new AuditTrailSpec();
                spec.AddMessage
                (
                    status: BusinessSpecStatus.InvalidInputs,
                    message: AuditTrailServiceSpecMessages.ExceptionNula
                );

                AddSpec(spec);

                return;
            }

            await _bus.CreateAsync(new Entities.AuditTrail(ex, date)).ConfigureAwait(false);
        }

        public Task CreateAsync(Exception ex) => CreateAsync(ex, DateTime.UtcNow);

        public async Task<IList<AuditTrailResponse>> GetByDateAsync(DateTime startDate, DateTime endDate)
            => (await _bus.GetByDateAsync(startDate, endDate).ConfigureAwait(false))
                .Select(at => (AuditTrailResponse)at)
                .ToList();
    }
}