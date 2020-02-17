using System;

namespace _4oito6.Infra.CrossCutting.Configuration.Token
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public string Issuer => Environment.GetEnvironmentVariable("Token__Issuer");

        public string Audience => Environment.GetEnvironmentVariable("Token__Audience");

        public string SecretKey => Environment.GetEnvironmentVariable("Token__SecretKey");

        public int TokenTime => Convert.ToInt32(Environment.GetEnvironmentVariable("Token__Time"));

        public int RefreshTokenTime => Convert.ToInt32(Environment.GetEnvironmentVariable("Token__RefreshTime"));
    }
}