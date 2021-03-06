﻿using _4oito6.Domain.Services.Core.Implementation.Base;
using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Domain.Model.ValueObjects;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Template.Domain.Services.Contracts.Interfaces;
using _4oito6.Template.Domain.Services.Contracts.Mapper;
using _4oito6.Template.Domain.Specs;
using _4oito6.Template.Domain.Specs.User;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Services;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Template.Domain.Services.Implementation
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserBus _userBus;
        private readonly IPhoneBus _phoneBus;
        private readonly IAddressBus _addressBus;

        public UserService(IUserBus userBus, IPhoneBus phoneBus, IAddressBus addressBus)
            : base(new IDisposable[] { userBus, phoneBus, addressBus })
        {
            _userBus = userBus ?? throw new ArgumentNullException(nameof(userBus));
            _phoneBus = phoneBus ?? throw new ArgumentNullException(nameof(phoneBus));
            _addressBus = addressBus ?? throw new ArgumentNullException(nameof(addressBus));
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            if (await _userBus.ExistsEmailAsync(request.Email).ConfigureAwait(false))
            {
                var spec = new CreateUserSpec();
                spec.AddMessage(BusinessSpecStatus.Conflict, UserServiceMessages.EmailJaCadastrado);

                AddSpec(spec);
                return null;
            }

            Address address = null;

            if (request.Address != null)
            {
                address = await _addressBus
                    .GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode)
                    .ConfigureAwait(false);

                if (address == null)
                {
                    address = new Address(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode);
                    AddSpec(new AddressSpec(address));
                }
            }

            IList<Phone> phones = new List<Phone>();

            if (request.Phones.Any())
            {
                phones = await _phoneBus
                    .GetByNumbersAsync
                    (
                        request.Phones
                            .Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number))
                            .ToList()
                    )
                    .ConfigureAwait(false);

                request.Phones
                    .Where(phone => !phones.Any(p => p.LocalCode == phone.LocalCode && p.Number == p.Number))
                    .ToList()
                    .ForEach(p =>
                    {
                        var newPhone = new Phone(p.LocalCode, p.Number);
                        AddSpec(new PhoneSpecs(newPhone));

                        phones.Add(newPhone);
                    });
            }

            var user = new User(new Name(request.FirstName, request.MiddleName, request.LastName), request.Email, request.Cpf, address, phones);
            AddSpec(new UserSpec(user));

            if (!IsSatisfied())
                return null;

            user = await _userBus.CreateUserAsync(user).ConfigureAwait(false);

            return user.ToResponse();
        }

        public async Task<UserResponse> UpdateUserAsync(UserRequest request)
        {
            var user = await _userBus.GetByIdAsync(request.Id ?? 0).ConfigureAwait(false);

            if (user == null)
            {
                var spec = new CreateUserSpec();
                spec.AddMessage(BusinessSpecStatus.ResourceNotFound, UserServiceMessages.UsuarioNaoEncontrado);

                AddSpec(spec);
                return null;
            }

            var token = await _userBus.GetTokenAsync().ConfigureAwait(false);

            if (token?.IdUser != user.Id)
            {
                var spec = new CreateUserSpec();
                spec.AddMessage(BusinessSpecStatus.Forbidden, UserServiceMessages.EditarProprioUsuario);

                AddSpec(spec);
                return null;
            }

            if (await _userBus.ExistsEmailAsync(request.Email, request.Id).ConfigureAwait(false))
            {
                var spec = new CreateUserSpec();
                spec.AddMessage(BusinessSpecStatus.Conflict, UserServiceMessages.EmailJaCadastrado);

                AddSpec(spec);
                return null;
            }

            Address address = null;

            if (request.Address != null)
            {
                address = await _addressBus
                    .GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode)
                    .ConfigureAwait(false);

                if (address == null)
                {
                    address = new Address(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode);
                    AddSpec(new AddressSpec(address));

                    user.ChangeAddress(address);
                }
            }
            else
            {
                user.RemoveAddress();
            }

            IList<Phone> phones = new List<Phone>();

            if (request.Phones.Any())
            {
                phones = await _phoneBus
                    .GetByNumbersAsync
                    (
                        request.Phones
                            .Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number))
                            .ToList()
                    )
                    .ConfigureAwait(false);

                request.Phones
                    .Where(phone => !phones.Any(p => p.LocalCode == phone.LocalCode && p.Number == p.Number))
                    .ToList()
                    .ForEach(p =>
                    {
                        var newPhone = new Phone(p.LocalCode, p.Number);
                        AddSpec(new PhoneSpecs(newPhone));

                        phones.Add(newPhone);
                    });

                user.ChangePhones(phones);
            }
            else
            {
                user.RemovePhones();
            }

            user.Update(request.FirstName, request.MiddleName, request.LastName, request.Email, request.Cpf);
            AddSpec(new UserSpec(user));

            if (!IsSatisfied())
                return null;

            await _userBus.UpdateUserAsync(user).ConfigureAwait(false);

            return new UserResponse { Id = user.Id };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userBus.GetByEmailAsync(request.Email).ConfigureAwait(false);

            if (user == null)
            {
                var spec = new LoginSpec();
                spec.AddMessage(BusinessSpecStatus.Forbidden, UserServiceMessages.NomeUsuarioInvalido);

                AddSpec(spec);
                return null;
            }

            var token = await _userBus.LoginAsync(user).ConfigureAwait(false);

            return new LoginResponse(token.Token, token.IdUser, token.Email, token.RefreshToken);
        }

        public async Task<LoginResponse> RefreshLoginAsync(string refreshToken)
        {
            if (!await _userBus.IsRefreshTokenValid(refreshToken).ConfigureAwait(false))
            {
                var spec = new LoginSpec();
                spec.AddMessage(BusinessSpecStatus.Unauthorized, UserServiceMessages.TokenExpirado);

                AddSpec(spec);
                return null;
            }

            var refresh = await _userBus.GetRefreshTokenAsync(refreshToken).ConfigureAwait(false);
            var user = await _userBus.GetByIdAsync(refresh.IdUser).ConfigureAwait(false);

            if (user == null)
            {
                var spec = new LoginSpec();
                spec.AddMessage(BusinessSpecStatus.Forbidden, UserServiceMessages.NomeUsuarioInvalido);

                AddSpec(spec);
                return null;
            }

            var token = await _userBus.RefreshLoginAsync(refreshToken, user).ConfigureAwait(false);

            return new LoginResponse(token.Token, token.IdUser, token.Email, token.RefreshToken);
        }
    }
}