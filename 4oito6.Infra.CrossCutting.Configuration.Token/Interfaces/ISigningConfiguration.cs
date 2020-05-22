using Microsoft.IdentityModel.Tokens;

namespace _4oito6.Infra.CrossCutting.Configuration.Token.Interfaces
{
    public interface ISigningConfiguration
    {
        SigningCredentials SigningCredentials { get; }
    }
}