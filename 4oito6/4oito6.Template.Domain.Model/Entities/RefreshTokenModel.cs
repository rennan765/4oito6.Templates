using _4oito6.Domain.Model.Core.Entities;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class RefreshTokenModel : EntityBase
    {
        public string RefreshToken { get; private set; }
        public RefreshTokenDataModel Data { get; private set; }

        public RefreshTokenModel(string refreshToken, RefreshTokenDataModel data)
        {
            RefreshToken = refreshToken;
            Data = data;
        }
    }
}