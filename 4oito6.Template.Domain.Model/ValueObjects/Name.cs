using _4oito6.Domain.Model.Core.ValueObjects;

namespace _4oito6.Template.Domain.Model.ValueObjects
{
    public class Name : ValueObjectBase
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }

        public Name(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
    }
}