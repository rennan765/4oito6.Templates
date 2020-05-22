namespace _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs
{
    public static class AddressSpecMessages
    {
        public static string LogradouroObrigatorio { get; private set; }
        public static string NumeroNaoNumerico { get; private set; }
        public static string BairroObrigatorio { get; private set; }

        public static string CidadeObrigatoria { get; private set; }
        public static string EstadoObrigatorio { get; private set; }
        public static string EstadoInvalido { get; private set; }

        public static string CepObrigatorio { get; private set; }
        public static string CepNaoNumerico { get; private set; }
        public static string CepInvalido { get; private set; }

        static AddressSpecMessages()
        {
            LogradouroObrigatorio = "O logradouro é obrigátório.";
            NumeroNaoNumerico = "Existe algum caractere não numérico no campo número.";
            BairroObrigatorio = "O bairro é obrigátório.";

            CidadeObrigatoria = "A cidade é obrigátória.";
            EstadoObrigatorio = "O estado é obrigátório.";
            EstadoInvalido = "O estado deve ser informado no  formato de sigla.";

            CepObrigatorio = "O CEP é obrigátório.";
            CepNaoNumerico = "Existe algum caractere não numérico no campo CEP.";
            CepInvalido = "O CEP precisa ter 8 caracteres.";
        }
    }
}