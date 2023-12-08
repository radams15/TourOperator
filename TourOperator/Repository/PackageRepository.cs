using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class PackageRepository: Repository
{
    public PackageRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Package', N'U') IS NULL BEGIN
            CREATE TABLE Package (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                RoomId INTEGER REFERENCES Room(Id),
                TourId INTEGER REFERENCES Tour(Id)
            );
        END;
        ")
    {
        
    }
}