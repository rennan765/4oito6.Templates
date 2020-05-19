namespace _4oito6.Infra.CrossCutting.Token.Models
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }

        public RefreshTokenData Data { get; set; }

        public RefreshTokenModel()
        {
        }

        public RefreshTokenModel(string refreshToken, RefreshTokenData data)
        {
            RefreshToken = refreshToken;
            Data = data;
        }
    }
}