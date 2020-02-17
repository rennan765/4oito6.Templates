namespace _4oito6.Infra.CrossCutting.Configuration.Token
{
    public interface ITokenConfiguration
    {
        string Issuer { get; }

        string Audience { get; }

        string SecretKey { get; }

        int TokenTime { get; }

        int RefreshTokenTime { get; }
    }
}