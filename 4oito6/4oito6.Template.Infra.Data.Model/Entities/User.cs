using _4oito6.Infra.Data.Model.Core;
using System.Collections.Generic;

namespace _4oito6.Template.Infra.Data.Model.Entities
{
    public class User : DataModelBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public long? IdAddress { get; set; }

        public virtual Address Address { get; set; }
        public virtual IList<UserPhone> Phones { get; set; }

        public User()
        {
            Phones = new List<UserPhone>();
        }
    }
}