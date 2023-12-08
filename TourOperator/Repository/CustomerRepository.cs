using System.Data;
using Microsoft.Data.SqlClient;
using TourOperator.Models;

namespace TourOperator.Repository;

public class CustomerRepository: Repository
{
    public CustomerRepository(string? connectionString)
        : base(connectionString, @"
         IF OBJECT_ID(N'dbo.Customer', N'U') IS NULL BEGIN
            CREATE TABLE Customer (
                Username VARCHAR(32),
                FullName VARCHAR(32),
                PassportNo INTEGER,
                PhoneNo INTEGER,
                Password VARCHAR(64)
            );
        END;
        ")
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

            cmd.Parameters.Add(@"Username", SqlDbType.VarChar).Value = username;
            
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
            
            cmd.Parameters.Add(@"Username", SqlDbType.VarChar).Value = customer.Username;
            cmd.Parameters.Add(@"Password", SqlDbType.VarChar).Value = customer.Password;
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        return GetCustomer(customer.Username);
    }
}