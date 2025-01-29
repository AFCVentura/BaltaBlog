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
        Console.WriteLine("String de Conexão: " + CONNECTION_STRING);
    }
}