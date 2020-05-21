namespace _4oito6.Template.Infra.CrossCutting.Messages.Domain.Services
{
    public static class UserServiceMessages
    {
        public static string EmailJaCadastrado { get; private set; }
        public static string UsuarioNaoEncontrado { get; private set; }

        public static string EditarProprioUsuario { get; private set; }
        public static string NomeUsuarioInvalido { get; private set; }
        public static string TokenExpirado { get; private set; }

        static UserServiceMessages()
        {
            EmailJaCadastrado = "E-mail já cadastrado.";
            UsuarioNaoEncontrado = "Usuário não encontrado.";

            EditarProprioUsuario = "Só é possível editar o próprio usuário.";
            NomeUsuarioInvalido = "Nome de usuário inválido.";
            TokenExpirado = "Token expirado.";
        }
    }
}