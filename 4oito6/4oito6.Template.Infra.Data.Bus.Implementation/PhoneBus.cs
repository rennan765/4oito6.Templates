using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Template.Infra.Data.Bus.Implementation
{
    public class PhoneBus : BusBase, IPhoneBus
    {
        private readonly IPhoneRepository _phoneRepository;

        public PhoneBus(IPhoneRepository phoneRepository)
            : base(new IDisposable[] { phoneRepository })
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
        }

        public async Task<IList<Phone>> GetByNumbersAsync(IList<Tuple<string, string>> numbers)
            => (await _phoneRepository.ListAsync(p => numbers.Any(n => n.Item1 == p.LocalCode && n.Item2 == p.Number)).ConfigureAwait(false))
                .Select(p => p.ToDomainModel())
                .ToList();
    }
}