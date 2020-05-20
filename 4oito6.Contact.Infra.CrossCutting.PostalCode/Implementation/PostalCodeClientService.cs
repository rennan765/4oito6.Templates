using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Interfaces;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.CrossCutting.PostalCode.Implementation
{
    public class PostalCodeClientService : IPostalCodeClientService
    {
        private HttpClient _client;
        private string _url;
        private bool _disposedValue;

        public PostalCodeClientService(HttpClient client, string url)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            //viacep.com.br/ws/{0}/json/
            _url = url ?? throw new ArgumentNullException(nameof(url));
            _disposedValue = false;
        }

        public async Task<AddressFromPostalCodeResponse> GetAddressAsync(string postalCode)
        {
            var response = await _client.GetAsync(string.Format(_url, postalCode)).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return (AddressFromPostalCodeResponse)JsonConvert.DeserializeObject<AddressFromPostalCode>
            (
                value: await response.Content.ReadAsStringAsync()
            );
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _client?.Dispose();
                    _client = null;
                }

                _url = null;

                _disposedValue = true;
            }
        }

        ~PostalCodeClientService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}