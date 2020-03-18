using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Threading.Tasks;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Infra.Data.Bus.Implementation
{
    public class UserBus : IUserBus
    {
        private IUserRepository _userRepository;
        private IPhoneRepository _phoneRepository;
        private IAddressRepository _addressRepository;
        private bool _disposedValue;

        public UserBus(IUserRepository userRepository, IPhoneRepository phoneRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<DomainModel.User> CreateUserAsync(DomainModel.User user)
            => (await _userRepository.InsertAsync(user.ToDataModel()).ConfigureAwait(false))
                .ToDomainModel();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _userRepository?.Dispose();
                    _userRepository = null;

                    _phoneRepository?.Dispose();
                    _phoneRepository = null;

                    _addressRepository?.Dispose();
                    _addressRepository = null;
                }

                _disposedValue = true;
            }
        }

        ~UserBus()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}