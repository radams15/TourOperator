using Microsoft.Data.SqlClient;

namespace TourOperator.Models;

public class Dao
{
    private readonly string _connectionString;

    protected Dao(string? connectionString)
    {
        _connectionString = connectionString
                           ?? throw new NullReferenceException("SQL connection string cannot be null!");
    }

    protected SqlConnection GetConnection()
    {
        Console.WriteLine($"Connection String: '{_connectionString}'");
        return new SqlConnection(_connectionString)
                   ?? throw new NullReferenceException("Could not connect to SQL Server!");
    }
}