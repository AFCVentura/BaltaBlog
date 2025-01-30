using BaltaBlog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BaltaBlog.Repositories;

public class UserRepository
{
    private readonly SqlConnection _connection;

    public UserRepository(SqlConnection connection)
        => _connection = connection;
    
    public IEnumerable<User> GetAll()
        => _connection.GetAll<User>();

    public User GetById(int id)
        => _connection.Get<User>(id);

    public long Create(User user)
        => _connection.Insert<User>(user);

    public bool Update(User user)
        => _connection.Update<User>(user);

    public bool Delete(User user)
    {
        _connection.Get<User>(2);
        return _connection.Delete<User>(user);
    }
}