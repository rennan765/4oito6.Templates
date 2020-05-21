namespace _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs.User
{
    public static class UserSpecMessages
    {
        public static string PrimeiroNomeObrigatorio { get; private set; }
        public static string SobrenomeObrigatorio { get; private set; }

        static UserSpecMessages()
        {
            PrimeiroNomeObrigatorio = "O primeiro nome é obrigatório.";
            SobrenomeObrigatorio = "O sobrenome é obrigatório.";
        }
    }
}