using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using System;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Interfaces
{
    public interface IPostalCodeClientService : IDisposable
    {
        Task<AddressFromPostalCodeResponse> GetAddressAsync(string postalCode);
    }
}