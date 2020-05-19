using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Template.Infra.Data.Bus.Implementation
{
    public class UserBus : BusBase, IUserBus
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenBuilderService _tokenBuilderService;

        public UserBus(IUnitOfWork unit, IUserRepository userRepository, ITokenBuilderService tokenBuilderService)
            : base(unit, new IDisposable[] { userRepository })
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenBuilderService = tokenBuilderService ?? throw new ArgumentNullException(nameof(tokenBuilderService));
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var newUser = await _userRepository.InsertAsync(user.ToDataModel()).ConfigureAwait(false);

            await Unit.SaveEntityChangesAsync().ConfigureAwait(false);

            return newUser.ToDomainModel();
        }

        public async Task<bool> ExistsEmailAsync(string email, int? idUser = null)
            => idUser == null ?
                await _userRepository.ExistsAsync(u => u.Email == email).ConfigureAwait(false) :
                await _userRepository.ExistsAsync(u => u.Email == email && u.Id != idUser).ConfigureAwait(false);

        public async Task<User> GetByEmailAsync(string email)
        {
            var dataModelUser = await _userRepository.GetAsync(u => u.Email == email).ConfigureAwait(false);

            if (dataModelUser == null)
                return null;

            return dataModelUser.ToDomainModel();
        }

        public async Task<User> GetByIdAsync(int id)
            => (await _userRepository.GetByIdAsync(id).ConfigureAwait(false))
                .ToDomainModel();

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(string refreshToken)
        {
            var token = await _tokenBuilderService.GetRefreshTokenAsync(refreshToken).ConfigureAwait(false);

            return new RefreshTokenModel
            (
                refreshToken: refreshToken,
                idUser: token.Data.Id,
                expiresOn: token.Data.ExpiresOn
            );
        }

        public async Task<TokenModel> GetTokenAsync()
        {
            var tokenModel = await _tokenBuilderService.GetTokenAsync().ConfigureAwait(false);

            return new TokenModel(tokenModel.Id, tokenModel.Email, null);
        }

        public async Task<TokenModel> LoginAsync(User user)
        {
            var token = await _tokenBuilderService.BuildTokenAsync(user.Id, user.Email, string.Empty)
                .ConfigureAwait(false);

            var tokenModel = await _tokenBuilderService.GetTokenAsync().ConfigureAwait(false);
            var refreshToken = await _tokenBuilderService.BuildRefreshTokenAsync(user.Id)
                .ConfigureAwait(false);

            return new TokenModel(tokenModel.Id, user.Email, token, refreshToken.RefreshToken);
        }

        public async Task<TokenModel> LoginByRefreshTokenAsync(string refreshToken, User user)
        {
            await _tokenBuilderService.RemoveRefreshTokenAsync(refreshToken).ConfigureAwait(false);

            return await LoginAsync(user).ConfigureAwait(false);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var userDb = await _userRepository.GetByIdAsync(user.Id).ConfigureAwait(false);

            userDb.FirstName = user.Name.FirstName;
            userDb.MiddleName = user.Name.MiddleName;
            userDb.LastName = user.Name.LastName;

            userDb.Email = user.Email;
            userDb.Cpf = user.Cpf;

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

            var updatedUser = await _userRepository.UpdateAsync(userDb).ConfigureAwait(false);

            await Unit.SaveEntityChangesAsync().ConfigureAwait(false);

            return updatedUser.ToDomainModel();
        }
    }
}