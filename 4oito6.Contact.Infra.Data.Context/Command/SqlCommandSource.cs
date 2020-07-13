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
                id,
	            logradouro,
	            numero,
	            complemento,
	            bairro,
	            cidade,
	            estado,
	            cep,
                U.id idusuario
            FROM endereco E
            INNER JOIN usuario U on U.idendereco = E.id
            ";

            Phone = @"
            SELECT
	            id,
	            ddd,
	            numero,
                UT.idusuario
            FROM telefone T
            INNER JOIN usuario_telefone UT ON UT.idtelefone = T.id
            ";
        }
    }
}