using System.Data;
using Microsoft.Data.SqlClient;

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
        END;
        ")
    {
        
    }
}