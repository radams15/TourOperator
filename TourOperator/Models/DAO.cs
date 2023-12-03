using Microsoft.Data.SqlClient;

namespace TourOperator.Models;

public class DAO
{
    private string ConnectionString;

    protected DAO(string connectionString)
    {
        ConnectionString = connectionString
                           ?? throw new NullReferenceException("SQL connection string cannot be null!");
    }

    protected SqlConnection GetConnection()
    {
        Console.WriteLine($"Connection String: '{ConnectionString}'");
        return new SqlConnection(ConnectionString)
                   ?? throw new NullReferenceException("Could not connect to SQL Server!");
    }
}