namespace _4oito6.Contact.Domain.Views
{
    public sealed class ViewAddress
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int IdUser { get; set; }
    }
}