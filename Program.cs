using BaltaBlog.Interfaces;
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

        // Read<User>(connection);
        // Read<Role>(connection);
        // Read<Tag>(connection);
        ReadUsersWithRoles(connection);

        // ReadUsers(connection);
        // ReadRoles(connection);

        connection.Close();
    }

    public static void Read<T>(SqlConnection connection) where T : class, IModel
    {
        var repository = new Repository<T>(connection);
        var models = repository.GetAll();
        foreach (var item in models)
        {
            Console.WriteLine(item.Id);
        }
    }

    public static void Create<T>(SqlConnection connection, T model) where T : class, IModel
    {
        var repository = new Repository<T>(connection);
        long createdId = repository.Create(model);
        Console.WriteLine($"Id criado: {createdId}");
    }

    public static void Update<T>(SqlConnection connection, T model) where T : class, IModel
    {
        var repository = new Repository<T>(connection);

        bool wasUpdated = repository.Update(model);

        Console.WriteLine(wasUpdated ? "Campo Atualizado" : "Campo não atualizado");
    }

    public static void Delete<T>(SqlConnection connection, int id) where T : class, IModel
    {
        var repository = new Repository<T>(connection);

        var model = repository.GetById(id);

        bool wasDeleted = repository.Delete(model);

        Console.WriteLine(wasDeleted ? "Campo Apagado" : "Campo não apagado");
    }

    public static void Delete<T>(SqlConnection connection, T model) where T : class, IModel
    {
        var repository = new Repository<T>(connection);

        bool wasDeleted = repository.Delete(model);

        Console.WriteLine(wasDeleted ? "Campo Apagado" : "Campo não apagado");
    }

    public static void ReadUsersWithRoles(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        var items = repository.GetWithRoles();

        foreach (var item in items)
        {
            Console.WriteLine(item.Name);
            foreach (var role in item.Roles)
            {
                Console.WriteLine($"- {role.Name}");
            }
        }
    }
}