using System;

namespace _4oito6.Infra.CrossCutting.Token.Models
{
    public class RefreshTokenData
    {
        

        public int Id { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public DateTime ExpiresOn { get; set; }

        public RefreshTokenData()
        {

        }

        public RefreshTokenData(int id, string email, string image, DateTime expiresOn)
        {
            Id = id;
            Email = email;
            Image = image;
            ExpiresOn = expiresOn;
        }

        public RefreshTokenData(TokenModel tokenModel, DateTime expiresOn)
        {
            Id = tokenModel.Id;
            Email = tokenModel.Email;
            Image = tokenModel.Image;
            ExpiresOn = expiresOn;
        }
    }
}
