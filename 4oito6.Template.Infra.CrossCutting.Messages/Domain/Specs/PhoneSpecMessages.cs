namespace _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs
{
    public static class PhoneSpecMessages
    {
        public static string DddObrigatorio { get; private set; }
        public static string DddInvalido { get; private set; }

        public static string NumeroObrigatorio { get; private set; }
        public static string NumeroInvalido { get; private set; }

        static PhoneSpecMessages()
        {
            DddObrigatorio = "O DDD é obrigatório.";
            DddInvalido = "O DDD precisa ter 2 caracteres.";

            NumeroObrigatorio = "O número é obrigatório.";
            NumeroInvalido = "O DDD precisa ter 8 ou 9 caracteres.";
        }
    }
}