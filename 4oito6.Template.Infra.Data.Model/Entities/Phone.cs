using _4oito6.Infra.Data.Model.Core;
using System.Collections.Generic;

namespace _4oito6.Template.Infra.Data.Model.Entities
{
    public class Phone : DataModelBase
    {
        public long Id { get; set; }
        public string LocalCode { get; set; }
        public string Number { get; set; }

        public virtual IList<UserPhone> Users { get; set; }

        public Phone()
        {
            Users = new List<UserPhone>();
        }
    }
}