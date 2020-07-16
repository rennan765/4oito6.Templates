namespace _4oito6.Contact.Infra.Data.Context.Command
{
    public static class SqlCommandSource
    {
        public static string Address { get; private set; }
        public static string Phone { get; private set; }

        static SqlCommandSource()
        {
            Address = @"
            SELECT
                E.id,
	            E.logradouro,
	            E.numero,
	            E.complemento,
	            E.bairro,
	            E.cidade,
	            E.estado,
	            E.cep,
                U.id idusuario
            FROM endereco E
            INNER JOIN usuario U on U.idendereco = E.id
            ";

            Phone = @"
            SELECT
	            T.id,
	            T.ddd,
	            T.numero,
                UT.idusuario
            FROM telefone T
            INNER JOIN usuario_telefone UT ON UT.idtelefone = T.id
            ";
        }
    }
}