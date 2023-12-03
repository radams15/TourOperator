using Microsoft.Data.SqlClient;

namespace TourOperator.Models;

public class UserDao: Dao
{
    public UserDao(string? connectionString)
        : base(connectionString)
    {
        Console.WriteLine("Init DAO!");
        InitTable();
    }

    private void InitTable()
    {
        string initString = @"
         IF OBJECT_ID(N'dbo.Customer', N'U') IS NULL BEGIN
            CREATE TABLE Customer (
                CustomerNumber INTEGER NOT NULL PRIMARY KEY,
                Username VARCHAR(32),
                FullName VARCHAR(32),
                PassportNo INTEGER,
                PhoneNo INTEGER,
                Password VARCHAR(32)
            );
        END;
        ";

        using (var conn = GetConnection())
        {
            SqlCommand cmd = new SqlCommand(initString, conn);
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
    
}