using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Implementation
{
    public class PhoneBus : BusBase, IPhoneBus
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ITokenBuilderService _tokenBuilderService;

        public PhoneBus(IUnitOfWork unit, IPhoneRepository phoneRepository, ITokenBuilderService tokenBuilderService)
            : base(unit, new IDisposable[] { phoneRepository })
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _tokenBuilderService = tokenBuilderService ?? throw new ArgumentNullException(nameof(tokenBuilderService));
        }

        public async Task<IList<Phone>> GetByLocalCodeAsync(string localCode)
        {
            return (await _phoneRepository.ListAsync(p => p.LocalCode == localCode).ConfigureAwait(false))
                .Select(p => p.ToDomainModel())
                .ToList();
        }

        public async Task<IList<Phone>> GetByUserAsync()
        {
            var token = _tokenBuilderService.GetToken();

            return (await _phoneRepository.ListAsync(p => p.Users.Any(up => up.IdUser == token.Id)).ConfigureAwait(false))
                .Select(p => p.ToDomainModel())
                .ToList();
        }
    }
}