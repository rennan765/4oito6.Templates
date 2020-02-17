namespace _4oito6.Infra.CrossCutting.Token.Models
{
    public class TokenModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public TokenModel()
        {
        }

        public TokenModel(int id, string email, string image)
        {
            Id = id;
            Email = email;
            Image = image;
        }
    }
}