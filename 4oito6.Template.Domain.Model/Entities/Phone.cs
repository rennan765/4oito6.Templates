using _4oito6.Domain.Model.Core.Entities;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class Phone : EntityBase
    {
        public long Id { get; private set; }
        public string LocalCode { get; private set; }
        public string Number { get; private set; }

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