using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel = _4oito6.Template.Infra.Data.Model.Entities;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Infra.Data.Bus.Implementation
{
    public class PhoneBus : BusBase, IPhoneBus
    {
        private readonly IPhoneRepository _phoneRepository;

        public PhoneBus(IUnitOfWork unit, IPhoneRepository phoneRepository)
            : base(unit, new IDisposable[] { phoneRepository })
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
        }

        public async Task<IList<DomainModel.Phone>> GetByNumbersAsync(IList<Tuple<string, string>> numbers)
        {
            var phones = new List<DataModel.Phone>();

            foreach (var number in numbers)
            {
                var phone = await _phoneRepository
                    .GetAsync(p => p.LocalCode == number.Item1 & p.Number == number.Item2)
                    .ConfigureAwait(false);

                if (phone != null)
                    phones.Add(phone);
            }

            return phones.Select(p => p.ToDomainModel()).ToList();
        }
    }
}