using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class HotelRepository: Repository
{
    public HotelRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Hotel', N'U') IS NULL BEGIN
            CREATE TABLE Hotel (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                OperatorId INTEGER REFERENCES Operator(Id),
                Name VARCHAR(32),
                Description VARCHAR(256),
                Spaces INTEGER
            );
        END;
        ")
    {
        
    }
}