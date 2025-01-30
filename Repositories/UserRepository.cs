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
    {
        user.Id = 0;
        return _connection.Insert<User>(user);
    }

    public bool Update(User user)
    {
        if (user.Id != 0)
            return _connection.Update<User>(user);

        return false;
    }

    public bool Delete(User user)
    {
        if (user.Id != 0)
            return _connection.Delete<User>(user);
        return false;
    }
    public bool Delete(int id)
    {
        if (id != 0)
        {
            var user = _connection.Get<User>(id);
            return _connection.Delete<User>(user);
        }
        return false;
    }
}