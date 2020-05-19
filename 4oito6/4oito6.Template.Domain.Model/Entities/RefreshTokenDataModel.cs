using System;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class RefreshTokenDataModel
    {
        public int IdUser { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public RefreshTokenDataModel(int idUser, DateTime expiresAt)
        {
            IdUser = idUser;
            ExpiresAt = expiresAt;
        }
    }
}