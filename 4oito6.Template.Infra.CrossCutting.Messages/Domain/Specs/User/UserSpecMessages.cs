namespace _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs.User
{
    public static class UserSpecMessages
    {
        public static string PrimeiroNomeObrigatorio { get; private set; }
        public static string SobrenomeObrigatorio { get; private set; }

        public static string CpfInvalido { get; private set; }
        public static string EmailInvalido { get; private set; }

        static UserSpecMessages()
        {
            PrimeiroNomeObrigatorio = "O primeiro nome é obrigatório.";
            SobrenomeObrigatorio = "O sobrenome é obrigatório.";

            CpfInvalido = "CPF inválido.";
            EmailInvalido = "E-mail inválido,";
        }
    }
}