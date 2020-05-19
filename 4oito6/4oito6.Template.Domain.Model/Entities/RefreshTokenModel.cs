using _4oito6.Domain.Model.Core.Entities;
using System;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class RefreshTokenModel : EntityBase
    {
        public string RefreshToken { get; private set; }
        public int IdUser { get; private set; }
        public DateTime ExpiresOn { get; private set; }

        public RefreshTokenModel(string refreshToken, int idUser, DateTime expiresOn)
        {
            RefreshToken = refreshToken;
            IdUser = idUser;
            ExpiresOn = expiresOn;
        }
    }
}