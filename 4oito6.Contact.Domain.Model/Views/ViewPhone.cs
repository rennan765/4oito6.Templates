namespace _4oito6.Contact.Domain.Model.Views
{
    public sealed class ViewPhone
    {
        public long Id { get; set; }
        public string LocalCode { get; set; }
        public string Number { get; set; }
        public int IdUser { get; set; }
    }
}