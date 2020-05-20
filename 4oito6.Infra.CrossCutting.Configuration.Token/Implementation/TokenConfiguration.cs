using _4oito6.Infra.CrossCutting.Configuration.Token.Interfaces;
using _4oito6.Infra.CrossCutting.Extensions;
using System;

namespace _4oito6.Infra.CrossCutting.Configuration.Token.Implementation
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public string Issuer => Environment.GetEnvironmentVariable("Token__Issuer");

        public string Audience => Environment.GetEnvironmentVariable("Token__Audience");

        public string SecretKey => Environment.GetEnvironmentVariable("Token__SecretKey");

        public int TokenTime 
            => Environment.GetEnvironmentVariable("Token__Time").ToInt() * 60;

        public int RefreshTokenTime 
            => Environment.GetEnvironmentVariable("Token__RefreshTime").ToInt() * 60;
    }
}