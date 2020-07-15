using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Contact.Infra.Data.Repositories.Contracts.Interfaces.Repositories;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Implementation
{
    public class PhoneBus : BusBase, IPhoneBus
    {
        private readonly IViewPhoneRepository _phoneRepository;
        private readonly ITokenBuilderService _tokenBuilderService;

        public PhoneBus(IUnitOfWork unit, IViewPhoneRepository phoneRepository, ITokenBuilderService tokenBuilderService)
            : base(unit, new IDisposable[] { phoneRepository })
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _tokenBuilderService = tokenBuilderService ?? throw new ArgumentNullException(nameof(tokenBuilderService));
        }

        public Task<IQueryable<ViewPhone>> GetByLocalCodeAsync(string localCode)
            => _phoneRepository.GetAsync(p => p.LocalCode == localCode);

        public async Task<IQueryable<ViewPhone>> GetByUserAsync()
        {
            var tokenModel = await _tokenBuilderService.GetTokenAsync().ConfigureAwait(false);

            return await _phoneRepository.GetAsync(p => p.IdUser == tokenModel.Id).ConfigureAwait(false);
        }
    }
}