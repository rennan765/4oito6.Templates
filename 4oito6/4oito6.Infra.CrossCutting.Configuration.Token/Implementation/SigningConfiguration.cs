using _4oito6.Infra.CrossCutting.Configuration.Token.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace _4oito6.Infra.CrossCutting.Configuration.Token.Implementation
{
    public class SigningConfiguration : ISigningConfiguration
    {
        public SigningCredentials SigningCredentials { get; private set; }

        public SigningConfiguration(ITokenConfiguration tokenConfiguration)
        {
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfiguration.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}