using BaltaBlog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaBlog.Repositories;

public class UserRepository : Repository<User>
{
    private readonly SqlConnection _connection;
    public UserRepository(SqlConnection connection) : base(connection)
        => _connection = connection;

    public List<User> GetWithRoles()
    {
        var query = @"
        SELECT
            [User].*,
            [Role].*
        FROM
            [User]
            LEFT JOIN [UserRole] ON [User].[Id] = [UserRole].[UserId]
            LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";

        var users = new List<User>();

        var items = _connection.Query<User, Role, User>(
            query,
            (user, role) =>
            {
                var usr = users.FirstOrDefault(x => x.Id == user.Id);

                if (usr is null)
                {
                    usr = user;
                    if (role is not null)
                        usr.Roles.Add(role);
                    users.Add(usr);
                }
                else
                    usr.Roles.Add(role);
                return user;
            }, splitOn: "Id");

        return users;
    }
}