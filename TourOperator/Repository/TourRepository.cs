using System.Data;
using Microsoft.Data.SqlClient;
using TourOperator.Models;

namespace TourOperator.Repository;

public class TourRepository: Repository
{
    public TourRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Tour', N'U') IS NULL BEGIN
            CREATE TABLE Tour (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                Spaces INTEGER,
                Price INTEGER,
                Length INTEGER,
                Name VARCHAR(32),
                Description VARCHAR(256)
            );

            INSERT INTO Tour (Spaces, Price, Length, Name, Description) VALUES (
                30, 120000, 6, 'Real Britain', 'Real Britain Tour'
            );
            
            INSERT INTO Tour (Spaces, Price, Length, Name, Description) VALUES (
                40, 200000, 16, 'Britain and Ireland Explorer', 'Britain and Ireland Explorer Tour'
            );
            
            INSERT INTO Tour (Spaces, Price, Length, Name, Description) VALUES (
                30, 290000, 12, 'Best of Britain', 'Best of Britain Tour'
            );
        END;
        ")
    {
        InitTable();
    }

    /*void CreateTour(Tour tour)
    {
        string sql = @"
            INSERT INTO Tour (Spaces, Price, Length, Name, Description) VALUES (
                @Spaces, @Price, @Length, @Name, @Description
            );
        ";

        using (var conn = GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            cmd.Parameters.Add(@"Spaces", SqlDbType.Int).Value = tour.Spaces;
            cmd.Parameters.Add(@"Price", SqlDbType.Int).Value = tour.Price;
            cmd.Parameters.Add(@"Length", SqlDbType.Int).Value = tour.Length;
            cmd.Parameters.Add(@"Name", SqlDbType.VarChar).Value = tour.Name;
            cmd.Parameters.Add(@"Description", SqlDbType.VarChar).Value = tour.Description;
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }*/
}