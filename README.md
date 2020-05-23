# 4oito6.Templates
Templates utilizados nos projetos 4oito6.
<hr />

Para debugar este projeto, restaure os pacotes nuget e execute-o.
Acesse a página http://localhost:{porta}/swagger para abrir a interface do swagger.
Caso queira abrir a interface do swagger minimizada, acessar http://localhost:{porta}/swagger/index.html?docExpansion=none.

<hr />
Este projeto utiliza variáveis de ambiente para o funcionamento. 
Para que o mesmo execute normalmente, será necessário inserir as variáveis abaixo: 

<table>
  <theader>
    <td>Nome</td>
    <td>Descrição</td>
    <td>Conteúdo de exemplo</td>
  </theader>
  <tr>
    <td>Token__Issuer</td>
    <td>Issuer do JWT</td>
    <td>c1f51f42</td>
  </tr>
  <tr>
    <td>Token__Audience</td>
    <td>Audience do JWT</td>
    <td>c6bbbb645024</td>
  </tr>
  <tr>
    <td>Token__SecretKey</td>
    <td>Secret key do JWT (normalmente um Guid)</td>
    <td>65ec8b25-6dee-4686-a139-f14029dd7f34</td>
  </tr>
  <tr>
    <td>Token__Time</td>
    <td>Tempo de vida do JWT (em minutos)</td>
    <td>15</td>
  </tr>
  <tr>
    <td>Token__RefreshTime</td>
    <td>Tempo de vida do refresh token (ainda não implementado neste template)</td>
    <td>3000</td>
  </tr>
  <tr>
    <td>DbConnectionString</td>
    <td>Connection string do banco de dados (neste exemplo pgsql)</td>
    <td>host=localhost;database=pgsqldb;user id=pgsqldb;pgsqldb</td>
  </tr>
  <tr>
    <td>SwaggerConfigModel</td>
    <td>Json de configuração da doc. do swagger. Seguir o exemplo ao lado</td>
    <td>{ \"Title\": \"Template API\", \"Version\": \"1\", \"Description\": \"API REST de template\", \"ContactName\": \"Your Name\", \"ContactEmail\": \"your@mail.com\", \"ContactUrl\": \"http://yourwebpage.com/\" }</td>
  </tr>
  <tr>
    <td>CacheConnectionString</td>
    <td>Connection string do REDIS, para armazenamento do refresh token</td>
    <td>127.0.0.1:6379,ssl=False,allowAdmin=True,abortConnect=False,defaultDatabase=0,connectTimeout=500,connectRetry=3</td>
  </tr>
  <tr>
    <td>CacheDbName</td>
    <td>Nome do banco do REDIS, para armazenamento do refresh token</td>
    <td>db1</td>
  </tr>
  <tr>
    <td>MongoConnectionString</td>
    <td>Connection string do MongoDB, para armazenamento de exceção</td>
    <td>mongodb+srv://{db}:{password}@my-server.com?retryWrites=true&w=majority</td>
  </tr>
  <tr>
    <td></td>
    <td>Nome da coleção do MongoDB, para armazenamento de exceção</td>
    <td>Exception</td>
  </tr>
</table>
