using BaltaBlog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace BaltaBlog.Repositories;

public class RoleRepository
{
    private readonly SqlConnection _connection;

    public RoleRepository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<Role> GetAll()
    => _connection.GetAll<Role>();

    public Role GetById(int id)
        => _connection.Get<Role>(id);

    public long Create(Role role)
        => _connection.Insert<Role>(role);

    public bool Update(Role role)
        => _connection.Update<Role>(role);

    public bool Delete(Role role)
    {
        _connection.Get<Role>(2);
        return _connection.Delete<Role>(role);
    }
}