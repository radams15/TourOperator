using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class RoomRepository: Repository
{
    public RoomRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Room', N'U') IS NULL BEGIN
            CREATE TABLE Room (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                HotelId INTEGER REFERENCES Hotel(Id),
                Name VARCHAR(32),
                Price INTEGER
            );
        END;
        ")
    {
        
    }
}