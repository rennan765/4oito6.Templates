using _4oito6.Infra.Data.Model.Core;

namespace _4oito6.Template.Infra.Data.Model.Entities
{
    public class UserPhone : DataModelBase
    {
        public long Id { get; private set; }
        public int IdUser { get; private set; }
        public long IdPhone { get; private set; }

        public virtual Phone Phone { get; private set; }
        public virtual User User { get; private set; }

        public UserPhone(long id, int idUser, long idPhone)
        {
            Id = id;
            IdUser = idUser;
            IdPhone = idPhone;
        }

        protected UserPhone()
        {
        }

        public UserPhone(long id, Phone phone, User user)
        {
            Id = id;
            Phone = phone;
            User = user;
        }

        public UserPhone(int idUser, long idPhone)
        {
            IdUser = idUser;
            IdPhone = idPhone;
        }

        public UserPhone(Phone phone, User user)
        {
            Phone = phone;
            User = user;
        }
    }
}