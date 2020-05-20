using System;

namespace _4oito6.Infra.CrossCutting.Token.Models
{
    public class RefreshTokenData
    {
        public int Id { get; set; }

        public DateTime ExpiresOn { get; set; }

        public RefreshTokenData()
        {
        }

        public RefreshTokenData(int id, DateTime expiresOn)
        {
            Id = id;
            ExpiresOn = expiresOn;
        }

        public RefreshTokenData(TokenModel tokenModel, DateTime expiresOn)
        {
            Id = tokenModel.Id;
            ExpiresOn = expiresOn;
        }
    }
}