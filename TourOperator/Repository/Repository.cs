using Microsoft.Data.SqlClient;

namespace TourOperator.Models;

public class Repository
{
    private readonly string _connectionString;

    protected Repository(string? connectionString)
    {
        _connectionString = connectionString
                           ?? throw new NullReferenceException("SQL connection string cannot be null!");
    }

    protected SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString)
                   ?? throw new NullReferenceException("Could not connect to SQL Server!");
    }
}