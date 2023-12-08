using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class Repository
{
    private readonly string _connectionString;
    private readonly string _initString;

    protected Repository(string? connectionString, string? initString)
    {
        _connectionString = connectionString
                           ?? throw new NullReferenceException("SQL connection string cannot be null!");
        _initString = initString;
    }

    protected SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString)
                   ?? throw new NullReferenceException("Could not connect to SQL Server!");
    }
    
    protected void InitTable()
    {
        using (var conn = GetConnection())
        {
            SqlCommand cmd = new SqlCommand(_initString, conn);
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}