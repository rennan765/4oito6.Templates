using System.Collections.Generic;

namespace _4oito6.Template.Domain.Services.Contracts.Arguments.Request
{
    public class UserRequest
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public AddressRequest Address { get; set; }
        public IList<UserPhoneRequest> Phones { get; set; }

        public UserRequest()
        {
            Phones = new List<UserPhoneRequest>();
        }
    }
}