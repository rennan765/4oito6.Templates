using _4oito6.Infra.Data.Model.Core;

namespace _4oito6.Template.Infra.Data.Model.Entities
{
    public class UserPhone : DataModelBase
    {
        protected UserPhone()
        {
        }

        public long Id { get; set; }
        public int IdUser { get; set; }
        public long IdPhone { get; set; }

        public virtual Phone Phone { get; set; }
        public virtual User User { get; set; }
    }
}