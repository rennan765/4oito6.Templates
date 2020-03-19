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
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using System;
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
            if (await _userBus.ExistsEmail(request.Email).ConfigureAwait(false))
            {
                var spec = new CreateUserSpec();
                spec.AddMessage(BusinessSpecStatus.Conflict, "E-mail já cadastrado.");

                AddSpec(spec);
                return null;
            }

            var address = await _addressBus
                .GetByInfoAsync(request.Street, request.Number, request.Complement, request.District, request.City, request.State, request.PostalCode).ConfigureAwait(false);

            if (address == null)
            {
                address = new Address(request.Street, request.Number, request.Complement, request.District, request.City, request.State, request.PostalCode);
                AddSpec(new AddressSpec(address));
            }

            var phones = await _phoneBus
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

            var user = new User(new Name(request.FirstName, request.MiddleName, request.LastName), request.Email, request.Cpf, address, phones);
            AddSpec(new UserSpec(user));

            if (!IsSatisfied())
                return null;

            user = await _userBus.CreateUserAsync(user).ConfigureAwait(false);

            return user.ToResponse();
        }
    }
}