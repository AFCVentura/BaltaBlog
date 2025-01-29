
# Projeto de Aprendizado de Dapper com .NET Core

Este projeto é um aplicativo de console desenvolvido para aprender e praticar o uso do **Dapper** com o **.NET Core** para realizar consultas em um **banco de dados SQL Server**. O objetivo é familiarizar-se com a biblioteca Dapper, que é uma ferramenta ORM (Object-Relational Mapping) leve e eficiente para acesso a dados no .NET.

## Requisitos

Antes de começar, certifique-se de ter o seguinte instalado:

- **.NET 6 ou superior** (para rodar o projeto)
- **SQL Server** (ou uma instância SQL Server configurada)
- **Visual Studio** ou outro editor de código de sua preferência
- **Dapper** (adicionado como dependência no projeto)

## Instalação

1. Clone este repositório:
   ```bash
   git clone <URL do seu repositório>
   cd <diretório do projeto>
   ```

2. Crie um banco de dados no SQL Server, se ainda não tiver um:
   - O nome do banco de dados pode ser `MyDatabase`, ou você pode alterar conforme necessário.
   
3. Crie a estrutura da tabela de exemplo. Aqui está um exemplo de script SQL para criar a tabela que será utilizada nas consultas:
   
   ```sql
   CREATE TABLE [dbo].[SampleTable] (
       [Id] INT PRIMARY KEY IDENTITY,
       [Name] NVARCHAR(100) NOT NULL,
       [Age] INT NOT NULL
   );
   ```

4. Adicione o pacote Dapper ao seu projeto via NuGet:
   ```bash
   dotnet add package Dapper
   ```

5. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`:
   
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=<SEU_SERVIDOR>;Database=<SEU_BANCO>;User Id=<SEU_USUARIO>;Password=<SUA_SENHA>;"
     }
   
   ```

   - Substitua `<SEU_SERVIDOR>`, `<SEU_BANCO>`, `<SEU_USUARIO>` e `<SUA_SENHA>` pelos valores corretos para o seu ambiente.

## Uso

1. **Consultas Simples:**
   O projeto demonstra como realizar consultas simples usando Dapper. Um exemplo de consulta para pegar todos os registros de uma tabela:

   ```csharp
   using (var connection = new SqlConnection(connectionString))
   {
       var result = connection.Query<SampleEntity>("SELECT * FROM SampleTable").ToList();
       foreach (var item in result)
       {
           Console.WriteLine($"{item.Name} - {item.Age}");
       }
   }
   ```

2. **Inserção de Dados:**
   A inserção de dados no banco de dados também é feita com Dapper. Exemplo de como adicionar um novo registro:

   ```csharp
   using (var connection = new SqlConnection(connectionString))
   {
       var insertQuery = "INSERT INTO SampleTable (Name, Age) VALUES (@Name, @Age)";
       var parameters = new { Name = "John Doe", Age = 30 };
       connection.Execute(insertQuery, parameters);
   }
   ```

3. **Consultas com Parâmetros:**
   Exemplos de consultas com parâmetros:

   ```csharp
   using (var connection = new SqlConnection(connectionString))
   {
       var query = "SELECT * FROM SampleTable WHERE Age = @Age";
       var parameters = new { Age = 30 };
       var result = connection.Query<SampleEntity>(query, parameters).ToList();
   }
   ```

4. **Atualizações e Exclusões:**
   Exemplo de atualização de registros:

   ```csharp
   using (var connection = new SqlConnection(connectionString))
   {
       var updateQuery = "UPDATE SampleTable SET Name = @Name WHERE Id = @Id";
       var parameters = new { Name = "Jane Doe", Id = 1 };
       connection.Execute(updateQuery, parameters);
   }
   ```

5. **Execução de Stored Procedures:**
   Se você tiver stored procedures, aqui está um exemplo de como executá-las com Dapper:

   ```csharp
   using (var connection = new SqlConnection(connectionString))
   {
       var result = connection.Query<SampleEntity>("usp_GetSampleData", commandType: CommandType.StoredProcedure).ToList();
   }
   ```

## Estrutura do Projeto

O projeto segue a estrutura simples:

```
/ProjectName
    /Models
        SampleEntity.cs
    /Services
        DatabaseService.cs
    appsettings.json
    Program.cs
    README.md
```

- **SampleEntity.cs**: Representa a entidade que será mapeada do banco de dados.
- **DatabaseService.cs**: Contém os métodos para interagir com o banco de dados usando Dapper.

## Contribuição

Se você gostaria de contribuir para este projeto, sinta-se à vontade para enviar um pull request. Sugestões de melhorias são bem-vindas!

## Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).

---

**Lacunas para preenchimento**:

- **URL do repositório**: Substitua `<URL do seu repositório>` com o link real do seu repositório no GitHub.
- **Estrutura da tabela**: Se sua tabela tiver mais ou diferentes colunas, ajuste o script SQL acima.
- **Entidade de exemplo**: Complete a classe `SampleEntity` com as propriedades que representam as colunas da sua tabela (como `Id`, `Name`, `Age`, etc.).

Esse `README.md` oferece uma visão geral de como configurar e usar o projeto, com espaço para você personalizar conforme o seu contexto específico.