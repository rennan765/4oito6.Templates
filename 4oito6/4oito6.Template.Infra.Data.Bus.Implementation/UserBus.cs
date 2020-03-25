using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Infra.Data.Bus.Implementation
{
    public class UserBus : BusBase, IUserBus
    {
        private IUserRepository _userRepository;
        private IPhoneRepository _phoneRepository;
        private IAddressRepository _addressRepository;

        public UserBus(IUnitOfWork unit, IUserRepository userRepository, IPhoneRepository phoneRepository, IAddressRepository addressRepository)
            : base(unit, new IDisposable[] { userRepository, phoneRepository, addressRepository })
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<DomainModel.User> CreateUserAsync(DomainModel.User user)
        {
            var newUser = await _userRepository.InsertAsync(user.ToDataModel()).ConfigureAwait(false);

            await Unit.SaveEntityChangesAsync().ConfigureAwait(false);

            return newUser.ToDomainModel();
        }

        public async Task<bool> ExistsEmailAsync(string email, int? idUser = null)
            => idUser == null ?
                await _userRepository.ExistsAsync(u => u.Email == email).ConfigureAwait(false) :
                await _userRepository.ExistsAsync(u => u.Email == email && u.Id != idUser).ConfigureAwait(false);

        public async Task<DomainModel.User> GetByIdAsync(int id)
            => (await _userRepository.GetByIdAsync(id).ConfigureAwait(false))
                .ToDomainModel();

        public async Task<DomainModel.User> UpdateUserAsync(DomainModel.User user)
        {
            var userDb = await _userRepository.GetByIdAsync(user.Id).ConfigureAwait(false);

            if (user.Address != null)
            {
                if (userDb.IdAddress != user.Address.Id)
                {
                    userDb.IdAddress = user.Address.Id > 0 ? user.Address.Id : userDb.IdAddress;
                    userDb.Address = user.Address.ToDataModel();
                }
            }
            else
            {
                userDb.IdAddress = null;
                userDb.Address = null;
            }

            //Remove who is in db and is not on screen
            userDb.Phones.ToList().ForEach(up =>
            {
                if (!user.Phones.Where(p => p.Id > 0).Any(p => p.Id == up.IdPhone))
                    userDb.Phones.Remove(up);
            });

            //Add who is on screen and is not in db
            user.Phones
                .Where(phone => phone.Id <= 0 || !userDb.Phones.Any(up => up.IdPhone == phone.Id))
                .ToList().ToDataModel(user).ToList()
                .ForEach(up => userDb.Phones.Add(up));

            var updatedUser = await _userRepository.UpdateAsync(user.ToDataModel()).ConfigureAwait(false);

            await Unit.SaveEntityChangesAsync().ConfigureAwait(false);

            return updatedUser.ToDomainModel();
        }
    }
}