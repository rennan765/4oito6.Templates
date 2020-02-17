using Newtonsoft.Json;

namespace _4oito6.Infra.CrossCutting.Token.Models
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }

        public string Data { get; set; }

        public RefreshTokenModel()
        {
        }

        public RefreshTokenModel(string refreshToken, string data)
        {
            RefreshToken = refreshToken;
            Data = data;
        }

        public RefreshTokenModel(string refreshToken, RefreshTokenData data)
        {
            RefreshToken = refreshToken;
            Data = JsonConvert.SerializeObject(data);
        }
    }
}