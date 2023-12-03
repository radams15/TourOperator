using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Models;

public class CustomerDao: Dao
{
    public CustomerDao(string? connectionString)
        : base(connectionString)
    {
        InitTable();
    }

    public Customer? GetCustomer(string username)
    {
        Customer? customer = null;
        
        string sql = @"
            SELECT * FROM Customer WHERE Username = @Username;
        ";

        using (var conn = GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = @"Username",
                Value = username,
                SqlDbType = SqlDbType.VarChar,
                Size = 32
            });
            
            conn.Open();
            
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.FieldCount > 0 && dataReader.Read())
                {
                    customer = new Customer()
                    {
                        Username = dataReader["Username"]?.ToString(),
                        Password = dataReader["Password"]?.ToString(),
                        FullName = dataReader["FullName"]?.ToString()
                    };
                    Int32.TryParse(dataReader["PassportNo"].ToString(), out customer.PassportNo);
                    Int32.TryParse(dataReader["PhoneNo"].ToString(), out customer.PhoneNo);
                }
            }   

            conn.Close();
        }

        return customer;
    }

    public Customer? CreateCustomer(Customer customer)
    {
        string sql = @"
            INSERT INTO Customer (Username, Password) VALUES (
                @Username, @Password
            );
        ";

        using (var conn = GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = @"Username",
                Value = customer.Username,
                SqlDbType = SqlDbType.VarChar,
                Size = 32
            });
            
            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = @"Password",
                Value = customer.Password,
                SqlDbType = SqlDbType.VarChar,
                Size = 64
            });
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        return GetCustomer(customer.Username);
    }

    private void InitTable()
    {
        string initString = @"
         IF OBJECT_ID(N'dbo.Customer', N'U') IS NULL BEGIN
            CREATE TABLE Customer (
                Username VARCHAR(32),
                FullName VARCHAR(32),
                PassportNo INTEGER,
                PhoneNo INTEGER,
                Password VARCHAR(64)
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