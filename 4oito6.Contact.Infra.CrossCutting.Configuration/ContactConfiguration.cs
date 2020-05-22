using System;

namespace _4oito6.Contact.Infra.CrossCutting.Configuration
{
    public class ContactConfiguration : IContactConfiguration
    {
        public string PostalCodeWsUrl => Environment.GetEnvironmentVariable("PostalCodeWsUrl");
    }
}