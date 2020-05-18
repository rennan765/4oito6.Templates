namespace _4oito6.Template.Domain.Services.Contracts.Arguments.Response
{
    public class LoginResponse
    {
        public object Token { get; set; }
        public int IdUser { get; set; }
        public string Email { get; set; }

        public LoginResponse()
        {
        }

        public LoginResponse(object token, int idUser, string email)
        {
            Token = token;
            IdUser = idUser;
            Email = email;
        }
    }
}