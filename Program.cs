using BaltaBlog.Models;
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
        
    }

    public static void ReadUsers()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            // Passa o Model e ele se vira  
            var users = connection.GetAll<User>();

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
    public static void ReadUser()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            // Passa dentro dos parênteses o Id que quer
            var user = connection.Get<User>(1);

            Console.WriteLine(user.Name);
        }
    }

    public static void CreateUser()
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

        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            long createdId = connection.Insert<User>(user);
            Console.WriteLine($"Id criado: {createdId}");
        }
    }

    public static void UpdateUser()
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

        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            bool wasUpdated = connection.Update<User>(user);

            Console.WriteLine(wasUpdated ? "Campo Atualizado" : "Campo não atualizado");
        }
    }
}