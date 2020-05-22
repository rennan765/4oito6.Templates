using _4oito6.AuditTrail.Infra.Data.Bus.Contracts;
using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Interfaces;
using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model;
using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Infra.Data.Bus.Implementation
{
    public class AuditTrailBus : BusBase, IAuditTrailBus
    {
        private readonly IAuditTrailMongoRepository _repository;

        public AuditTrailBus(IUnitOfWork unit, IAuditTrailMongoRepository repository)
            : base(unit, new IDisposable[] { })
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateAsync(Domain.Entities.AuditTrail auditTrail)
        {
            var dto = auditTrail.ToAuditTrailDto();

            await _repository.CreateAsync(dto).ConfigureAwait(false);
        }

        public async Task<IList<Domain.Entities.AuditTrail>> GetByDateAsync(DateTime startDate, DateTime endDate)
            => (await _repository.GetByDateAsync(startDate, endDate).ConfigureAwait(false))
                .Select(dto => dto.ToAuditTrail())
                .ToList();
    }
}