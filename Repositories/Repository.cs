using BaltaBlog.Interfaces;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace BaltaBlog.Repositories;

public class Repository<T> where T : class, IModel // O T deve implementar IModel
{
    private readonly SqlConnection _connection;

    public Repository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<T> GetAll()
        => _connection.GetAll<T>(); // Só funciona se tiver o where lá em cima

    public T GetById(int id)
    => _connection.Get<T>(id);

    public long Create(T model)
    {
        model.Id = 0;
        return _connection.Insert<T>(model);
    }
    public bool Update(T model)
    {
        if (model.Id != 0)
            return _connection.Update<T>(model);

        return false;
    }

    public bool Delete(T model)
    {
        if (model.Id != 0)
            return _connection.Delete<T>(model);
        return false;
    }
    public bool Delete(int id)
    {
        if (id != 0)
        {
            var model = _connection.Get<T>(id);
            return _connection.Delete<T>(model);
        }
        return false;
    }
}