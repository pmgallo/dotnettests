using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace EFCoreVsDapper.Database;

public class SqliteConnectionFactory
{
    private string? _connectionString;

    public SqliteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        SqlMapper.RemoveTypeMap(typeof(Guid));
        SqlMapper.RemoveTypeMap(typeof(Guid?));
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}