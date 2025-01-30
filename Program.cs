using BaltaBlog.Models;
using BaltaBlog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BaltaBlog;

public class Program
{
    private static readonly string CONNECTION_STRING;

    static Program()
    {
        // Configuração do appsettings.json e variáveis de ambiente
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Define a raiz do projeto
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Recuperando a string de conexão
        CONNECTION_STRING = config["ConnectionStrings:CONNECTION_STRING"];
    }
    public static void Main(string[] args)
    {
        var connection = new SqlConnection(CONNECTION_STRING);
        connection.Open();

        ReadUsers(connection);

        connection.Close();
    }

    public static void ReadUsers(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        var users = repository.GetAll();

        foreach (var user in users)
        {
            Console.WriteLine(user.Name);
        }
    }


    public static void CreateUser(SqlConnection connection)
    {
        var user = new User()
        {
            Name = "Isabela Ventura",
            Email = "isa.ventura@gmail.com",
            PasswordHash = "HASH",
            Bio = "Oii",
            Image = "https://",
            Slug = "isabela-ventura"
        };
        var repository = new UserRepository(connection);

        long createdId = repository.Create(user);

        Console.WriteLine($"Id criado: {createdId}");
    }

    public static void UpdateUser(SqlConnection connection)
    {
        var user = new User()
        {
            Id = 2,
            Name = "Isabela Ventura",
            Email = "isa.ventura1304@gmail.com",
            PasswordHash = "HASH",
            Bio = "Oi",
            Image = "https://",
            Slug = "isabela-ventura"
        };

        var repository = new UserRepository(connection);

        bool wasUpdated = repository.Update(user);
        
        Console.WriteLine(wasUpdated ? "Campo Atualizado" : "Campo não atualizado");
    }

    public static void DeleteUser(SqlConnection connection)
    {
        var repository = new UserRepository(connection);

        var user = repository.GetById(2);

        bool wasDeleted = repository.Delete(user);

        Console.WriteLine(wasDeleted ? "Campo Apagado" : "Campo não apagado");
    }
}