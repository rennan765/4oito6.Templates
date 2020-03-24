using _4oito6.Infra.Data.Model.Core;
using System.Collections.Generic;

namespace _4oito6.Template.Infra.Data.Model.Entities
{
    public class Phone : DataModelBase
    {
        public long Id { get; private set; }
        public string LocalCode { get; private set; }
        public string Number { get; private set; }

        public virtual IList<UserPhone> Users { get; private set; }

        protected Phone()
        {
            Users = new List<UserPhone>();
        }

        public Phone(long id, string localCode, string number, IList<UserPhone> users)
        {
            Id = id;
            LocalCode = localCode;
            Number = number;
            Users = users;
        }

        public Phone(long id, string localCode, string number)
        {
            Id = id;
            LocalCode = localCode;
            Number = number;
        }

        public Phone(string localCode, string number)
        {
            LocalCode = localCode;
            Number = number;
        }
    }
}