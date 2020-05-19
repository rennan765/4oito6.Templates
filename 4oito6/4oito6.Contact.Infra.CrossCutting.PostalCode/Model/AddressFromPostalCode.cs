using Newtonsoft.Json;

namespace _4oito6.Contact.Infra.CrossCutting.PostalCode.Model
{
    public class AddressFromPostalCode
    {
        [JsonProperty(PropertyName = "cep")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "logradouro")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "complemento")]
        public string Complement { get; set; }

        [JsonProperty(PropertyName = "bairro")]
        public string District { get; set; }

        [JsonProperty(PropertyName = "localidade")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "uf")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "unidade")]
        public string Unity { get; set; }

        [JsonProperty(PropertyName = "ibge")]
        public string Ibge { get; set; }

        [JsonProperty(PropertyName = "gia")]
        public string Gia { get; set; }
    }
}